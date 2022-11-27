using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.Entities;
using Backend.DTOs;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public UserService(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Object> GetUser(string username)
        {
            var user = await _userRepository.GetUser(username);
            var roles = await _userRepository.GetRoles(user);
            string role = roles.FirstOrDefault();

            switch (role)
            {
                case "Student":
                    var student = await _userRepository.GetStudent(user.Id);
                    return new StudentDto
                    {
                        ActorType = "Student",
                        UserName = user.UserName,
                        Email = user.Email,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        EntranceYear = student.EntranceYear,
                    };
                case "User":
                    break;
                default:
                    break;
            }

            return null;
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
