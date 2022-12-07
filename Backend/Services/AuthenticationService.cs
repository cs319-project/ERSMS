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
using Backend.Utilities.Enum;

namespace Backend.Services
{
    public class AuthenticationService : DataContextService, IAuthenticationService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthenticationService(
            DataContext dataContext,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<Role> roleManager
        ) : base(dataContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Register(RegisterDto register)
        {
            try
            {
                string actorType = register.ActorType;
                AppUser user = new AppUser(register, actorType);

                switch (actorType)
                {
                    case "Student":
                        // var student = (user.DomainUser as Student);
                        // student.Majors = new List<DepartmentInfo>();
                        // foreach (DepartmentInfoDto dep in register.Majors)
                        // {
                        //     student.Majors.Add(new DepartmentInfo()
                        //     {
                        //         DepartmentName = EnumStringify.DepartmentEnumarator(dep.DepartmentName),
                        //         FacultyName = EnumStringify.FacultyEnumarator(dep.FacultyName)
                        //     });
                        // }
                        // student.ExchangeSchool = register.ExchangeSchool;
                        break;
                    case "Exchange Coordinator":
                        var exchangeCoordinator = (user.DomainUser as ExchangeCoordinator);
                        exchangeCoordinator.Department = new DepartmentInfo();
                        exchangeCoordinator.Department.DepartmentName = EnumStringify.DepartmentEnumarator(register.Department.DepartmentName);
                        // t.Department.FacultyName = EnumStringify.FacultyEnumarator(register.Department.FacultyName);
                        break;
                    case "Admin":
                        break;
                    case "Dean Department Chair":
                        var deanDepartmentChair = (user.DomainUser as DeanDepartmentChair);
                        deanDepartmentChair.Department = new DepartmentInfo();
                        deanDepartmentChair.Department.DepartmentName = EnumStringify.DepartmentEnumarator(register.Department.DepartmentName);
                        // deanDepartmentChair.Department.FacultyName = EnumStringify.FacultyEnumarator(register.Department.FacultyName);
                        deanDepartmentChair.IsDean = register.IsDean;
                        break;
                    case "Course Coordinator Instructor":
                        var courseCoordinatorInstructor = (user.DomainUser as CourseCoordinatorInstructor);
                        courseCoordinatorInstructor.Department = new DepartmentInfo();
                        courseCoordinatorInstructor.Department.DepartmentName = EnumStringify.DepartmentEnumarator(register.Department.DepartmentName);
                        // courseCoordinatorInstructor.Department.FacultyName = EnumStringify.FacultyEnumarator(register.Department.FacultyName);
                        courseCoordinatorInstructor.IsCourseCoordinator = register.IsCourseCoordinator;
                        break;
                    case "Office of International Students and Exchange Programs":
                        break;
                    default:
                        throw new System.ArgumentException("Invalid actor type");
                }

                // if (register is CoordinatorRegisterDto)
                // {
                //     var department = EnumStringify.DepartmentEnumarator("CS");
                //     if (user.DomainUser.GetType() == typeof(ExchangeCoordinator))
                //     {
                //         (user.DomainUser as ExchangeCoordinator).Department.DepartmentName = department;
                //     }
                // }

                var result = await _userManager.CreateAsync(user, register.Password);
                return result.Succeeded ? await AssignRole(user, actorType) : result;
            }
            catch (System.ArgumentException e)
            {
                return IdentityResult.Failed(new IdentityError { Description = e.Message });
            }
        }

        public Task<IdentityResult> AssignRole(AppUser user, string actorType)
        {
            return _userManager.AddToRoleAsync(user, actorType);
        }

        public async Task<SignInResult> LogIn(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user is null) return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync(user, login.Password, isPersistent: false, lockoutOnFailure: false);
        }

        public Task LogOut()
        {
            return _signInManager.SignOutAsync();
        }

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

        public DomainUser GetUserById(string id)
        {
            var user = _dataContext.DomainUsers.SingleOrDefault(x => x.IdentityUser.UserName == id);
            return user;
        }

        public Task<IdentityResult> ChangePassword(AppUser user, string currentPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public Task<IdentityResult> ForceChangePassword(AppUser user, string newPassword)
        {
            _userManager.RemovePasswordAsync(user);
            return _userManager.AddPasswordAsync(user, newPassword);
        }
    }
}
