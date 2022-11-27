using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Interfaces
{
    public interface IUserService
    {
        void Update(AppUser user);
        Task<Object> GetUser(string username);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        // Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        // Task<MemberDto> GetMemberAsync(string username);
    }
}
