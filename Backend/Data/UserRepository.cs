using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        private readonly UserManager<AppUser> _userManager;

        public UserRepository(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public void Update(AppUser user)
        {
            throw new NotImplementedException();
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
    }
}
