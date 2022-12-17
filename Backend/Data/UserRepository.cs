using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<AppUser> GetUser(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<Student> GetStudent(Guid id)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        public async Task<Student> GetStudentByUserName(string userName)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.IdentityUser.UserName == userName);
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> DeleteStudent(Guid id)
        {
            Student student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<ExchangeCoordinator> GetExchangeCoordinator(Guid id)
        {
            return await _context.ExchangeCoordinators.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        public async Task<Admin> GetAdmin(Guid id)
        {
            return await _context.Admins.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        public async Task<DeanDepartmentChair> GetDeanDepartmentChair(Guid id)
        {
            return await _context.DeanDepartmentChairs.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        public async Task<CourseCoordinatorInstructor> GetCourseCoordinatorInstructor(Guid id)
        {
            return await _context.CourseCoordinatorInstructors.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        public async Task<OISEP> GetOISEP(Guid id)
        {
            return await _context.OISEPs.FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        public async Task<IList<String>> GetRoles(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<String> GetUserRole(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }

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

        public async Task<ExchangeCoordinator> GetExchangeCoordinatorByUserName(string username)
        {
            var coordinator = await _context.ExchangeCoordinators.FirstOrDefaultAsync(x => x.IdentityUser.UserName == username);
            return coordinator;
        }

        public async Task<IEnumerable<ExchangeCoordinator>> GetExchangeCoordinatorsAsync()
        {
            return await _context.ExchangeCoordinators.ToListAsync();
        }

        public async Task<IEnumerable<DeanDepartmentChair>> GetDeanDepartmentChairsAsync()
        {
            return await _context.DeanDepartmentChairs.ToListAsync();
        }

        public async Task<IEnumerable<CourseCoordinatorInstructor>> GetCourseCoordinatorInstructorsAsync()
        {
            return await _context.CourseCoordinatorInstructors.ToListAsync();
        }

        public async Task<bool> UpdateDomainUser(DomainUser domainUser)
        {
            _context.DomainUsers.Update(domainUser);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<DomainUser>> GetDomainUsers()
        {
            return await _context.DomainUsers.ToListAsync();
        }

        public async Task<bool> DeleteUser(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
