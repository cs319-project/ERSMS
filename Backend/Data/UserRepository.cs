using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Backend.Data
{
    /// <summary>A repository for users.</summary>
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        /// <summary>Creates a new instance of the <see cref="UserRepository"/> class.</summary>
        /// <param name="context">The <see cref="DataContext"/> to use.</param>
        /// <param name="userManager">The <see cref="UserManager{AppUser}"/> to use.</param>
        /// <param name="mapper">The <see cref="IMapper"/> to use.</param>
        public UserRepository(DataContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>Gets the user with the specified username.</summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user with the specified username.</returns>
        public async Task<AppUser> GetUser(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        /// <summary>Gets a student by their ID.</summary>
        /// <param name="id">The ID of the student.</param>
        /// <returns>The student with the specified ID.</returns>
        public async Task<Student> GetStudent(Guid id)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        /// <summary>Gets a student by their user name.</summary>
        /// <param name="userName">The user name of the student.</param>
        /// <returns>The student with the specified user name.</returns>
        public async Task<Student> GetStudentByUserName(string userName)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.IdentityUser.UserName == userName);
        }

        /// <summary>Gets all students.</summary>
        /// <returns>All students.</returns>
        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        /// <summary>Deletes a student.</summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>The deleted student.</returns>
        public async Task<Student> DeleteStudent(Guid id)
        {
            Student student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        /// <summary>Gets the exchange coordinator for the current user.</summary>
        /// <param name="id">The user's ID.</param>
        /// <returns>The exchange coordinator for the current user.</returns>
        public async Task<ExchangeCoordinator> GetExchangeCoordinator(Guid id)
        {
            return await _context.ExchangeCoordinators.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        /// <summary>Gets an admin by their ID.</summary>
        /// <param name="id">The ID of the admin.</param>
        /// <returns>The admin with the specified ID.</returns>
        public async Task<Admin> GetAdmin(Guid id)
        {
            return await _context.Admins.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        /// <summary>Gets the dean of the department.</summary>
        /// <param name="id">The ID of the dean.</param>
        /// <returns>The dean of the department.</returns>
        public async Task<DeanDepartmentChair> GetDeanDepartmentChair(Guid id)
        {
            return await _context.DeanDepartmentChairs.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        /// <summary>Gets the course coordinator instructor.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The course coordinator instructor.</returns>
        public async Task<CourseCoordinatorInstructor> GetCourseCoordinatorInstructor(Guid id)
        {
            return await _context.CourseCoordinatorInstructors.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        /// <summary>Gets the OISEP with the specified ID.</summary>
        /// <param name="id">The ID of the OISEP.</param>
        /// <returns>The OISEP with the specified ID.</returns>
        public async Task<OISEP> GetOISEP(Guid id)
        {
            return await _context.OISEPs.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        /// <summary>Gets the roles of a user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>The roles of the user.</returns>
        public async Task<IList<String>> GetRoles(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        /// <summary>Gets the user role.</summary>
        /// <param name="user">The user.</param>
        /// <returns>The user role.</returns>
        public async Task<String> GetUserRole(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }

        /// <summary>Gets the domain user.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The domain user.</returns>
        public async Task<DomainUser> GetDomainUser(string userName)
        {
            var result = await _context.DomainUsers.FirstOrDefaultAsync(x => x.IdentityUser.UserName == userName);
            return result;
        }

        public Task<AppUser> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>Updates the user's information.</summary>
        /// <param name="dto">The user's new information.</param>
        /// <returns>The updated user.</returns>
        public async Task<Object> UpdateUser(JObject dto)
        {
            var role = dto["actorType"].ToString();

            switch (role)
            {
                // case "Student":
                //     var student = (Student)dto;
                //     _context.Students.Update(student);
                //     break;
                case "Exchange Coordinator":
                    var exchangeCoordinator = _context.ExchangeCoordinators.SingleAsync(x => x.Id == Guid.Parse(dto["id"].ToString())).Result;
                    exchangeCoordinator.FirstName = dto["firstName"].ToString();
                    exchangeCoordinator.LastName = dto["lastName"].ToString();
                    exchangeCoordinator.IdentityUser.Email = dto["identityUser"]["email"].ToString();
                    exchangeCoordinator.IdentityUser.UserName = dto["identityUser"]["userName"].ToString();
                    exchangeCoordinator.Department = new DepartmentInfo();
                    exchangeCoordinator.Department.DepartmentName = EnumStringify.DepartmentEnumarator(dto["department"]["departmentName"].ToString());
                    exchangeCoordinator.Department.FacultyName = EnumStringify.FacultyEnumarator(dto["department"]["facultyName"].ToString());
                    _context.ExchangeCoordinators.Update(exchangeCoordinator);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<ExchangeCoordinator, ExchangeCoordinatorDto>(exchangeCoordinator);
                case "Admin":
                    var admin = _context.Admins.SingleAsync(x => x.Id == Guid.Parse(dto["id"].ToString())).Result;
                    admin.FirstName = dto["firstName"].ToString();
                    admin.LastName = dto["lastName"].ToString();
                    admin.IdentityUser.Email = dto["identityUser"]["email"].ToString();
                    admin.IdentityUser.UserName = dto["identityUser"]["userName"].ToString();
                    _context.Admins.Update(admin);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<Admin, AdminDto>(admin);
                case "Dean Department Chair":
                    var deanDepartmentChair = _context.DeanDepartmentChairs.SingleAsync(x => x.Id == Guid.Parse(dto["id"].ToString())).Result;
                    deanDepartmentChair.FirstName = dto["firstName"].ToString();
                    deanDepartmentChair.LastName = dto["lastName"].ToString();
                    deanDepartmentChair.IdentityUser.Email = dto["identityUser"]["email"].ToString();
                    deanDepartmentChair.IdentityUser.UserName = dto["identityUser"]["userName"].ToString();
                    deanDepartmentChair.Department = new DepartmentInfo();
                    deanDepartmentChair.Department.DepartmentName = EnumStringify.DepartmentEnumarator(dto["department"]["departmentName"].ToString());
                    deanDepartmentChair.Department.FacultyName = EnumStringify.FacultyEnumarator(dto["department"]["facultyName"].ToString());
                    deanDepartmentChair.IsDean = bool.Parse(dto["isDean"].ToString());
                    _context.DeanDepartmentChairs.Update(deanDepartmentChair);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<DeanDepartmentChair, DeanDepartmentChairDto>(deanDepartmentChair);
                case "Course Coordinator Instructor":
                    var courseCoordinatorInstructor = _context.CourseCoordinatorInstructors.SingleAsync(x => x.Id == Guid.Parse(dto["id"].ToString())).Result;
                    courseCoordinatorInstructor.FirstName = dto["firstName"].ToString();
                    courseCoordinatorInstructor.LastName = dto["lastName"].ToString();
                    courseCoordinatorInstructor.IdentityUser.Email = dto["identityUser"]["email"].ToString();
                    courseCoordinatorInstructor.IdentityUser.UserName = dto["identityUser"]["userName"].ToString();
                    courseCoordinatorInstructor.Department = new DepartmentInfo();
                    courseCoordinatorInstructor.Department.DepartmentName = EnumStringify.DepartmentEnumarator(dto["department"]["departmentName"].ToString());
                    courseCoordinatorInstructor.Department.FacultyName = EnumStringify.FacultyEnumarator(dto["department"]["facultyName"].ToString());
                    courseCoordinatorInstructor.Course = new Course();
                    courseCoordinatorInstructor.Course.CourseCode = dto["course"]["courseCode"].ToString();
                    courseCoordinatorInstructor.Course.CourseName = dto["course"]["courseName"].ToString();
                    courseCoordinatorInstructor.IsCourseCoordinator = bool.Parse(dto["isCourseCoordinator"].ToString());
                    _context.CourseCoordinatorInstructors.Update(courseCoordinatorInstructor);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<CourseCoordinatorInstructor, CourseCoordinatorInstructorDto>(courseCoordinatorInstructor);
                case "OISEP":
                    var oisep = _context.OISEPs.SingleAsync(x => x.Id == Guid.Parse(dto["id"].ToString())).Result;
                    oisep.FirstName = dto["firstName"].ToString();
                    oisep.LastName = dto["lastName"].ToString();
                    oisep.IdentityUser.Email = dto["identityUser"]["email"].ToString();
                    oisep.IdentityUser.UserName = dto["identityUser"]["userName"].ToString();
                    _context.OISEPs.Update(oisep);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<OISEP, OISEPDto>(oisep);
                default:
                    return null;
            }
        }

        /// <summary>Gets the exchange coordinator by user name.</summary>
        /// <param name="username">The username.</param>
        /// <returns>The exchange coordinator.</returns>
        public async Task<ExchangeCoordinator> GetExchangeCoordinatorByUserName(string username)
        {
            var coordinator = await _context.ExchangeCoordinators.FirstOrDefaultAsync(x => x.IdentityUser.UserName == username);
            return coordinator;
        }

        /// <summary>Gets the exchange coordinators.</summary>
        /// <returns>The exchange coordinators.</returns>
        public async Task<IEnumerable<ExchangeCoordinator>> GetExchangeCoordinatorsAsync()
        {
            return await _context.ExchangeCoordinators.ToListAsync();
        }

        /// <summary>Gets the dean and department chairs.</summary>
        /// <returns>The dean and department chairs.</returns>
        public async Task<IEnumerable<DeanDepartmentChair>> GetDeanDepartmentChairsAsync()
        {
            return await _context.DeanDepartmentChairs.ToListAsync();
        }

        /// <summary>Gets the course coordinator instructors.</summary>
        /// <returns>The course coordinator instructors.</returns>
        public async Task<IEnumerable<CourseCoordinatorInstructor>> GetCourseCoordinatorInstructorsAsync()
        {
            return await _context.CourseCoordinatorInstructors.ToListAsync();
        }

        /// <summary>Updates a domain user.</summary>
        /// <param name="domainUser">The domain user to update.</param>
        /// <returns>Whether the update was successful.</returns>
        public async Task<bool> UpdateDomainUser(DomainUser domainUser)
        {
            _context.DomainUsers.Update(domainUser);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets the domain users.</summary>
        /// <returns>The domain users.</returns>
        public async Task<IEnumerable<DomainUser>> GetDomainUsers()
        {
            return await _context.DomainUsers.ToListAsync();
        }

        /// <summary>Deletes a user.</summary>
        /// <param name="username">The username of the user to delete.</param>
        /// <returns>True if the user was deleted, false otherwise.</returns>
        public async Task<bool> DeleteUser(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets all placed students.</summary>
        /// <returns>All placed students.</returns>
        public async Task<IEnumerable<PlacedStudent>> GetPlacedStudentsAsync()
        {
            return await _context.PlacedStudents.ToListAsync();
        }

        public async Task<DeanDepartmentChair> GetDeanDepartmentChairByUserName(string username)
        {
            return await _context.DeanDepartmentChairs.FirstOrDefaultAsync(x => x.IdentityUser.UserName == username);
        }
    }
}
