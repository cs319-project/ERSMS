using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Utilities.Enum;
using Newtonsoft.Json.Linq;

namespace Backend.Services
{
    /// <summary>A service for user operations.</summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="UserService"/> class.</summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="mapper">The mapper.</param>
        public UserService(IUserRepository userRepository, IAuthenticationService authenticationService, IMapper mapper)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        /// <summary>Gets the exchange coordinator for the specified user.</summary>
        /// <param name="username">The username.</param>
        /// <returns>The exchange coordinator.</returns>
        public async Task<ExchangeCoordinator> GetExchangeCoordinator(string username)
        {
            var coordinator = await _userRepository.GetExchangeCoordinatorByUserName(username);
            return coordinator;
        }

        /// <summary>Gets a student by username.</summary>
        /// <param name="username">The username.</param>
        /// <returns>The student.</returns>
        public async Task<Student> GetStudent(string username)
        {
            var student = await _userRepository.GetStudentByUserName(username);
            return student;
        }

        /// <summary>Retrieves the user with the specified username.</summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The user with the specified username.</returns>
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

        /// <summary>Gets the exchange coordinators by department.</summary>
        /// <param name="department">The department.</param>
        /// <returns>The exchange coordinators by department.</returns>
        public async Task<IEnumerable<ExchangeCoordinatorDto>> GetExchangeCoordinatorsByDepartmentAsync(Department department)
        {
            var users = await _userRepository.GetExchangeCoordinatorsAsync();
            IEnumerable<ExchangeCoordinator> usersByDepartment = users.Where(x => x.Department.DepartmentName == department);
            return _mapper.Map<IEnumerable<ExchangeCoordinatorDto>>(usersByDepartment);
        }

        /// <summary>Gets the dean and department chairs for a given department.</summary>
        /// <param name="department">The department.</param>
        /// <returns>The dean and department chairs for the given department.</returns>
        public async Task<IEnumerable<DeanDepartmentChairDto>> GetDeanDepartmentChairsByDepartmentAsync(Department department)
        {
            var users = await _userRepository.GetDeanDepartmentChairsAsync();
            IEnumerable<DeanDepartmentChair> usersByDepartment = users.Where(x => x.Department.DepartmentName == department);
            return _mapper.Map<IEnumerable<DeanDepartmentChairDto>>(usersByDepartment);
        }

        /// <summary>Gets the course coordinators and instructors by course code.</summary>
        /// <param name="courseCode">The course code.</param>
        /// <returns>The course coordinators and instructors by course code.</returns>
        public async Task<IEnumerable<CourseCoordinatorInstructorDto>> GetCourseCoordinatorsInstructorsByCourseCodeAsync(string courseCode)
        {
            var users = await _userRepository.GetCourseCoordinatorInstructorsAsync();
            // TODO: check null
            IEnumerable<CourseCoordinatorInstructor> usersByCourseCode = users.Where(x => x.Course.CourseCode == courseCode);
            return _mapper.Map<IEnumerable<CourseCoordinatorInstructorDto>>(usersByCourseCode);
        }

        /// <summary>Gets the students.</summary>
        /// <returns>The students.</returns>
        public async Task<IEnumerable<StudentDto>> GetStudentsAsync()
        {
            return _mapper.Map<IEnumerable<StudentDto>>(await _userRepository.GetStudentsAsync());
        }

        /// <summary>Gets a user by their ID.</summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user with the specified ID.</returns>
        public Task<AppUser> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>Gets a user by their username.</summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user with the specified username.</returns>
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUser(username);
        }

        /// <summary>Gets the users.</summary>
        /// <returns>The users.</returns>
        public async Task<IEnumerable<DomainUserDto>> GetUsers()
        {
            var users = await _userRepository.GetDomainUsers();
            return _mapper.Map<IEnumerable<DomainUserDto>>(users);
        }

        /// <summary>Gets the domain user by username.</summary>
        /// <param name="username">The username.</param>
        /// <returns>The domain user.</returns>
        public async Task<DomainUser> GetDomainUser(string username)
        {
            var user = await _userRepository.GetDomainUser(username);
            return user;
        }

        /// <summary>Updates the user.</summary>
        /// <param name="dto">The DTO which contains the new user information.</param>
        /// <returns>The user.</returns>
        public async Task<Object> UpdateUser(JObject dto)
        {
            var user = await _userRepository.UpdateUser(dto);
            return (user != null) ? user : null;
        }

        /// <summary>Deletes a user.</summary>
        /// <param name="username">The username of the user to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task<bool> DeleteUser(string username)
        {
            return await _userRepository.DeleteUser(username);
        }

        /// <summary>Gets the placed students.</summary>
        /// <returns>The placed students.</returns>
        public async Task<IEnumerable<PlacedStudentDto>> GetPlacedStudents()
        {
            var forms = await _userRepository.GetPlacedStudentsAsync();
            return _mapper.Map<IEnumerable<PlacedStudentDto>>(forms);
        }

        /// <summary>Gets the students from the same school.</summary>
        /// <param name="username">The username.</param>
        /// <returns>The students from the same school.</returns>
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

        /// <summary>Gets the students by department.</summary>
        /// <param name="department">The department.</param>
        /// <returns>The students by department.</returns>
        public async Task<IEnumerable<StudentDto>> GetStudentsByDepartmentAsync(Department department)
        {
            var students = await _userRepository.GetStudentsAsync();
            var studentsByDepartment = students.Where(x => x.Major.DepartmentName == department);
            return _mapper.Map<IEnumerable<StudentDto>>(studentsByDepartment);
        }

        /// <summary>Gets the dean of the department.</summary>
        /// <param name="userName">The user name of the dean.</param>
        /// <returns>The dean of the department.</returns>
        public async Task<DeanDepartmentChairDto> GetDean(string userName)
        {
            var deanDepartmentChair = await _userRepository.GetDeanDepartmentChairByUserName(userName);
            if (deanDepartmentChair.IsDean)
            {
                return _mapper.Map<DeanDepartmentChairDto>(deanDepartmentChair);
            }
            else
            {
                return null;
            }
        }

        /// <summary>Gets the department chair of the user.</summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The department chair of the user.</returns>
        public async Task<DeanDepartmentChairDto> GetDepartmentChair(string userName)
        {
            var deanDepartmentChair = await _userRepository.GetDeanDepartmentChairByUserName(userName);
            if (!deanDepartmentChair.IsDean)
            {
                return _mapper.Map<DeanDepartmentChairDto>(deanDepartmentChair);
            }
            else
            {
                return null;
            }
        }

        public async Task<ICollection<StudentDto>> GetRegisteredStudentsTuples()
        {
            var students = await _userRepository.GetStudentsAsync();
            var studentsTuples = students.Select(s => new MailUserNameTupleDto
            { Email = s.IdentityUser.Email, UserName = s.IdentityUser.UserName }).ToList();
            return _mapper.Map<ICollection<StudentDto>>(studentsTuples);
        }
    }
}
