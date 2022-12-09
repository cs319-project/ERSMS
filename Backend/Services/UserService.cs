using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.Entities;
using Backend.DTOs;
using AutoMapper;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IAuthenticationService authenticationService, IMapper mapper)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public Task<ExchangeCoordinator> GetCoordinator(string username)
        {
            var coordinator = _userRepository.GetCoordinatorByUserName(username);
            return coordinator;
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
                    var studentDto = _mapper.Map<StudentDto>(student);
                    return studentDto;
                case "Exchange Coordinator":
                    var exchangeCoordinator = await _userRepository.GetExchangeCoordinator(user.Id);
                    var exchangeCoordinatorDto = _mapper.Map<ExchangeCoordinatorDto>(exchangeCoordinator);
                    return exchangeCoordinatorDto;
                case "Admin":
                    var admin = await _userRepository.GetAdmin(user.Id);
                    var adminDto = _mapper.Map<AdminDto>(admin);
                    return adminDto;
                case "Dean Department Chair":
                    var deanDepartmentChair = await _userRepository.GetDeanDepartmentChair(user.Id);
                    var deanDepartmentChairDto = _mapper.Map<DeanDepartmentChairDto>(deanDepartmentChair);
                    return deanDepartmentChairDto;
                case "Course Coordinator Instructor":
                    var courseCoordinatorInstructor = await _userRepository.GetCourseCoordinatorInstructor(user.Id);
                    var courseCoordinatorInstructorDto = _mapper.Map<CourseCoordinatorInstructorDto>(courseCoordinatorInstructor);
                    return courseCoordinatorInstructorDto;
                case "Office of International Students and Exchange Programs":
                    var oisep = await _userRepository.GetOISEP(user.Id);
                    var oisepDto = _mapper.Map<OISEPDto>(oisep);
                    return oisepDto;
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
