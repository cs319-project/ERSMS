using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.DTOs;
using Newtonsoft.Json.Linq;

namespace Backend.Interfaces
{
    /// <summary>An interface for the user repository.</summary>
    public interface IUserRepository
    {
        Task<Object> UpdateUser(JObject dto);
        Task<AppUser> GetUser(string username);
        Task<Student> GetStudent(Guid id);
        Task<ExchangeCoordinator> GetExchangeCoordinator(Guid id);
        Task<ExchangeCoordinator> GetExchangeCoordinatorByUserName(string username);
        Task<Admin> GetAdmin(Guid id);
        Task<DeanDepartmentChair> GetDeanDepartmentChair(Guid id);
        Task<DeanDepartmentChair> GetDeanDepartmentChairByUserName(string username);
        Task<CourseCoordinatorInstructor> GetCourseCoordinatorInstructor(Guid id);
        Task<OISEP> GetOISEP(Guid id);
        Task<IList<String>> GetRoles(AppUser user);
        Task<String> GetUserRole(AppUser user);
        Task<DomainUser> GetDomainUser(string userName);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<Student> GetStudentByUserName(string userName);
        Task<IEnumerable<ExchangeCoordinator>> GetExchangeCoordinatorsAsync();
        Task<IEnumerable<DeanDepartmentChair>> GetDeanDepartmentChairsAsync();
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<IEnumerable<PlacedStudent>> GetPlacedStudentsAsync();
        Task<IEnumerable<CourseCoordinatorInstructor>> GetCourseCoordinatorInstructorsAsync();
        Task<bool> UpdateDomainUser(DomainUser domainUser);
        Task<IEnumerable<DomainUser>> GetDomainUsers();
        Task<bool> DeleteUser(string username);
    }
}
