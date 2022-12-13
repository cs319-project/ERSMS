using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Entities;
using Backend.Utilities.Enum;

namespace Backend.Interfaces
{
    public interface IUserService
    {
        void Update(AppUser user);
        Task<Object> GetUser(string username);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<ExchangeCoordinator> GetExchangeCoordinator(string username);
        Task<Student> GetStudent(string username);
        Task<IEnumerable<ExchangeCoordinatorDto>> GetExchangeCoordinatorsByDepartmentAsync(Department department);
        Task<IEnumerable<DeanDepartmentChairDto>> GetDeanDepartmentChairsByDepartmentAsync(Department department);
        Task<IEnumerable<CourseCoordinatorInstructorDto>> GetCourseCoordinatorsInstructorsByCourseCodeAsync(string courseCode);
        Task<IEnumerable<StudentDto>> GetStudentsAsync();
    }
}
