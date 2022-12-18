using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.Entities;
using Backend.DTOs;
using AutoMapper;
using Backend.Utilities.Enum;
using Newtonsoft.Json.Linq;

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

        public async Task<ExchangeCoordinator> GetExchangeCoordinator(string username)
        {
            var coordinator = await _userRepository.GetExchangeCoordinatorByUserName(username);
            return coordinator;
        }

        public async Task<Student> GetStudent(string username)
        {
            var student = await _userRepository.GetStudentByUserName(username);
            return student;
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
                    var exchangeCoordinatorDto = _mapper.Map<ExchangeCoordinator, ExchangeCoordinatorDto>(exchangeCoordinator);
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

        public async Task<IEnumerable<ExchangeCoordinatorDto>> GetExchangeCoordinatorsByDepartmentAsync(Department department)
        {
            var users = await _userRepository.GetExchangeCoordinatorsAsync();
            IEnumerable<ExchangeCoordinator> usersByDepartment = users.Where(x => x.Department.DepartmentName == department);
            return _mapper.Map<IEnumerable<ExchangeCoordinatorDto>>(usersByDepartment);
        }

        public async Task<IEnumerable<DeanDepartmentChairDto>> GetDeanDepartmentChairsByDepartmentAsync(Department department)
        {
            var users = await _userRepository.GetDeanDepartmentChairsAsync();
            IEnumerable<DeanDepartmentChair> usersByDepartment = users.Where(x => x.Department.DepartmentName == department);
            return _mapper.Map<IEnumerable<DeanDepartmentChairDto>>(usersByDepartment);
        }

        public async Task<IEnumerable<CourseCoordinatorInstructorDto>> GetCourseCoordinatorsInstructorsByCourseCodeAsync(string courseCode)
        {
            var users = await _userRepository.GetCourseCoordinatorInstructorsAsync();
            // TODO: check null
            IEnumerable<CourseCoordinatorInstructor> usersByCourseCode = users.Where(x => x.Course.CourseCode == courseCode);
            return _mapper.Map<IEnumerable<CourseCoordinatorInstructorDto>>(usersByCourseCode);
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsAsync()
        {
            return _mapper.Map<IEnumerable<StudentDto>>(await _userRepository.GetStudentsAsync());
        }

        public Task<AppUser> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUser(username);
        }

        public async Task<IEnumerable<DomainUserDto>> GetUsers()
        {
            var users = await _userRepository.GetDomainUsers();
            return _mapper.Map<IEnumerable<DomainUserDto>>(users);
        }

        public void Update(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<DomainUser> GetDomainUser(string username)
        {
            var user = await _userRepository.GetDomainUser(username);
            return user;
        }

        public async Task<Object> UpdateUser(JObject dto)
        {
            var user = await _userRepository.UpdateUser(dto);
            return (user != null) ? user : null;
        }

        public async Task<bool> DeleteUser(string username)
        {
            return await _userRepository.DeleteUser(username);
        }

        public async Task<IEnumerable<PlacedStudentDto>> GetPlacedStudents()
        {
            var forms = await _userRepository.GetPlacedStudentsAsync();
            return _mapper.Map<IEnumerable<PlacedStudentDto>>(forms);
        }

        public async Task<ICollection<StudentDto>> GetStudentsFromSameSchool(string username)
        {
            var students = await _userRepository.GetStudentsAsync();
            var curStudent = await _userRepository.GetStudentByUserName(username);
            List<StudentDto> sameSchoolStudents = new List<StudentDto>();

            if (curStudent == null || string.IsNullOrEmpty(curStudent.ExchangeSchool)) return null;

            foreach (var student in students)
            {
                if (!string.IsNullOrEmpty(student.ExchangeSchool) && student.ExchangeSchool == curStudent.ExchangeSchool)
                {
                    sameSchoolStudents.Add(_mapper.Map<StudentDto>(student));
                }
            }

            return sameSchoolStudents;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsByDepartmentAsync(Department department)
        {
            var students = await _userRepository.GetStudentsAsync();
            var studentsByDepartment = students.Where(x => x.Major.DepartmentName == department);
            return _mapper.Map<IEnumerable<StudentDto>>(studentsByDepartment);
        }
    }
}
