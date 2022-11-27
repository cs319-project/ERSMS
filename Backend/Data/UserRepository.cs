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

        public async Task<IList<String>> GetRoles(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
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
    }
}
