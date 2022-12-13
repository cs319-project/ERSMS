using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<AppUser> GetUser(string username);
        Task<Student> GetStudent(Guid id);
        Task<ExchangeCoordinator> GetExchangeCoordinator(Guid id);
        Task<ExchangeCoordinator> GetExchangeCoordinatorByUserName(string username);
        Task<Admin> GetAdmin(Guid id);
        Task<DeanDepartmentChair> GetDeanDepartmentChair(Guid id);
        Task<CourseCoordinatorInstructor> GetCourseCoordinatorInstructor(Guid id);
        Task<OISEP> GetOISEP(Guid id);
        Task<IList<String>> GetRoles(AppUser user);
        Task<String> GetUserRole(AppUser user);
        Task<DomainUser> GetDomainUser(string userName);

        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<Student> GetStudentByUserName(string userName);
        // Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        // Task<MemberDto> GetMemberAsync(string username);
    }
}
