using Backend.DTOs;
using Backend.Entities;
using Backend.Utilities.Enum;
using Newtonsoft.Json.Linq;

namespace Backend.Interfaces
{
    /// <summary>An interface for the user service.</summary>
    public interface IUserService
    {
        Task<Object> GetUser(string username);
        Task<IEnumerable<DomainUserDto>> GetUsers();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<ExchangeCoordinator> GetExchangeCoordinator(string username);
        Task<Student> GetStudent(string username);
        Task<IEnumerable<PlacedStudentDto>> GetPlacedStudents();
        Task<DomainUser> GetDomainUser(string username);
        Task<IEnumerable<ExchangeCoordinatorDto>> GetExchangeCoordinatorsByDepartmentAsync(Department department);
        Task<IEnumerable<DeanDepartmentChairDto>> GetDeanDepartmentChairsByDepartmentAsync(Department department);
        Task<DeanDepartmentChairDto> GetDean(String userName);
        Task<DeanDepartmentChairDto> GetDepartmentChair(String userName);
        Task<IEnumerable<CourseCoordinatorInstructorDto>> GetCourseCoordinatorsInstructorsByCourseCodeAsync(string courseCode);
        Task<IEnumerable<StudentDto>> GetStudentsByDepartmentAsync(Department department);
        Task<IEnumerable<StudentDto>> GetStudentsAsync();
        Task<Object> UpdateUser(JObject dto);
        Task<bool> DeleteUser(string username);
        Task<ICollection<StudentDto>> GetStudentsFromSameSchool(string username);
    }
}
