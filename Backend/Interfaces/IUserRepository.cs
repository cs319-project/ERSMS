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
        Task<Admin> GetAdmin(Guid id);
        Task<DeanDepartmentChair> GetDeanDepartmentChair(Guid id);
        Task<CourseCoordinatorInstructor> GetCourseCoordinatorInstructor(Guid id);
        Task<OISEP> GetOISEP(Guid id);
        Task<IList<String>> GetRoles(AppUser user);

        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        // Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        // Task<MemberDto> GetMemberAsync(string username);
    }
}
