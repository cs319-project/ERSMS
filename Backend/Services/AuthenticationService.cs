using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Entities;
using Backend.Interfaces;
using Backend.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Backend.Utilities;
using Microsoft.EntityFrameworkCore;
using Backend.Utilities.Enum;

namespace Backend.Services
{
    /// <summary>A service for authentication operations.</summary>
    public class AuthenticationService : DataContextService, IAuthenticationService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPlacementRepository _placementRepository;
        private readonly ITokenService _tokenService;

        /// <summary>Initializes a new instance of the <see cref="AuthenticationService"/> class.</summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="placementRepository">The placement repository.</param>
        /// <param name="tokenService">The token service.</param>
        public AuthenticationService(
            DataContext dataContext,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<Role> roleManager,
            IPlacementRepository placementRepository,
            ITokenService tokenService
        ) : base(dataContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _placementRepository = placementRepository;
            _tokenService = tokenService;
        }

        /// <summary>Registers a new user.</summary>
        /// <param name="register">The user's registration information.</param>
        /// <returns>The user's authentication result.</returns>
        /// <exception cref="Exception">Thrown when the user already exists.</exception>
        public async Task<AuthenticationResultDto> Register(RegisterDto register)
        {
            try
            {
                if (await UserExists(register.UserName, register.Email)) throw new Exception("A user already exists with the same username or email address");

                register.Email = register.Email.ToLower();
                register.UserName = register.UserName.ToLower();

                string actorType = register.ActorType;
                AppUser user = new AppUser(register, actorType);

                switch (actorType)
                {
                    case "Student":
                        var student = (user.DomainUser as Student);
                        try
                        {
                            var studentInfo = await _placementRepository.GetPlacedStudent(register.UserName);

                            // If student is not placed, abort registration and return an informative error message
                            if (studentInfo.IsPlaced == false)
                            {
                                throw new Exception("Student has been found on the placement table, but he/she is not placed to a school yet");
                            }

                            // Set entrance year according to first two digits of student ID
                            student.EntranceYear = Int32.Parse("20" + studentInfo.UserName.Substring(1, 2));

                            student.FirstName = studentInfo.FirstName;
                            student.LastName = studentInfo.LastName;
                            student.Major = new DepartmentInfo();
                            student.Major.DepartmentName = studentInfo.Department.DepartmentName;
                            student.Major.FacultyName = studentInfo.Department.FacultyName;
                            student.ExchangeSchool = studentInfo.ExchangeSchool;
                            student.PreferredSemester = studentInfo.PreferredSemester;
                            student.PreferredSchools = studentInfo.PreferredSchools;
                            student.CGPA = studentInfo.CGPA;
                            student.ExchangeScore = studentInfo.ExchangeScore;
                            student.ToDoList = ToDoListSeeder.studentToDoListChecklistSeeding();

                        }
                        catch (Exception e)
                        {
                            throw new Exception(String.Format("Student not found in placement repository: {0}", e.Message));
                        }

                        break;
                    case "Exchange Coordinator":
                        var exchangeCoordinator = (user.DomainUser as ExchangeCoordinator);
                        exchangeCoordinator.Department = new DepartmentInfo();
                        exchangeCoordinator.Department.DepartmentName = EnumStringify.DepartmentEnumarator(register.Department.DepartmentName);
                        exchangeCoordinator.Department.FacultyName = EnumStringify.FacultyEnumarator(register.Department.FacultyName);
                        break;
                    case "Admin":
                        break;
                    case "Dean Department Chair":
                        var deanDepartmentChair = (user.DomainUser as DeanDepartmentChair);
                        deanDepartmentChair.Department = new DepartmentInfo();
                        deanDepartmentChair.Department.DepartmentName = EnumStringify.DepartmentEnumarator(register.Department.DepartmentName);
                        deanDepartmentChair.Department.FacultyName = EnumStringify.FacultyEnumarator(register.Department.FacultyName);
                        deanDepartmentChair.IsDean = register.IsDean;
                        break;
                    case "Course Coordinator Instructor":
                        var courseCoordinatorInstructor = (user.DomainUser as CourseCoordinatorInstructor);
                        courseCoordinatorInstructor.Department = new DepartmentInfo();
                        courseCoordinatorInstructor.Department.DepartmentName = EnumStringify.DepartmentEnumarator(register.Department.DepartmentName);
                        courseCoordinatorInstructor.Department.FacultyName = EnumStringify.FacultyEnumarator(register.Department.FacultyName);
                        courseCoordinatorInstructor.Course = new Course();
                        courseCoordinatorInstructor.Course.CourseCode = register.Course.CourseCode;
                        courseCoordinatorInstructor.Course.CourseName = register.Course.CourseName;
                        courseCoordinatorInstructor.IsCourseCoordinator = register.IsCourseCoordinator;
                        break;
                    case "Office of International Students and Exchange Programs":
                        break;
                    default:
                        throw new System.ArgumentException("Invalid actor type");
                }

                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, actorType);
                }
                else
                {
                    throw new Exception("User creation failed: " + result.Errors.First().Description);
                }

                var dto = new AuthenticationResultDto
                {
                    UserName = register.UserName,
                    Email = register.Email,
                    Token = null,
                    UserDetails = null
                };

                return dto;
            }
            catch (System.ArgumentException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Assigns a role to a user.</summary>
        /// <param name="user">The user to assign the role to.</param>
        /// <param name="actorType">The role to assign.</param>
        /// <returns>The <see cref="IdentityResult"/> of the operation.</returns>
        public Task<IdentityResult> AssignRole(AppUser user, string actorType)
        {
            return _userManager.AddToRoleAsync(user, actorType);
        }

        /// <summary>Logs in a user.</summary>
        /// <param name="login">The login information.</param>
        /// <returns>The authentication result.</returns>
        /// <exception cref="Exception">Thrown when the user is not found or the password is invalid.</exception>
        public async Task<AuthenticationResultDto> LogIn(LoginDto login)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user is null) throw new Exception("User not found");

                var result = await _signInManager.PasswordSignInAsync(user, login.Password, isPersistent: false, lockoutOnFailure: false);

                if (!result.Succeeded) throw new Exception("Invalid password");

                var dto = new AuthenticationResultDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user),
                    UserDetails = null
                };

                return dto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Signs the user out.</summary>
        /// <returns>A task that completes when the sign out operation is complete.</returns>
        public Task LogOut()
        {
            return _signInManager.SignOutAsync();
        }

        /// <summary>Creates the roles.</summary>
        public async Task CreateRoles()
        {
            var roles = EnumStringify.IdentityRoleList();
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new Role { Name = role });
                }
            }
        }

        /// <summary>Gets a user by their ID.</summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user with the specified ID.</returns>
        public DomainUser GetUserById(string id)
        {
            var user = _dataContext.DomainUsers.SingleOrDefault(x => x.IdentityUser.UserName == id);
            return user;
        }

        /// <summary>Changes the password for the specified user.</summary>
        /// <param name="user">The user whose password should be changed.</param>
        /// <param name="currentPassword">The current password for the user.</param>
        /// <param name="newPassword">The new password for the user.</param>
        /// <returns>The <see cref="IdentityResult"/> of the operation.</returns>
        public Task<IdentityResult> ChangePassword(AppUser user, string currentPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        /// <summary>Forcefully changes the password for the specified user.</summary>
        /// <param name="user">The user whose password will be changed.</param>
        /// <param name="newPassword">The new password for the user.</param>
        /// <returns>The <see cref="IdentityResult"/> of the operation.</returns>
        public Task<IdentityResult> ForceChangePassword(AppUser user, string newPassword)
        {
            _userManager.RemovePasswordAsync(user);
            return _userManager.AddPasswordAsync(user, newPassword);
        }


        /// <summary>Checks if a user exists in the database.</summary>
        /// <param name="username">The username to check.</param>
        /// <param name="email">The email to check.</param>
        /// <returns>Whether the user exists.</returns>
        public async Task<bool> UserExists(string username, string email)
        {
            return await _userManager.Users.AnyAsync(x => (x.UserName == username.ToLower() || x.Email == email.ToLower()));
        }
    }
}
