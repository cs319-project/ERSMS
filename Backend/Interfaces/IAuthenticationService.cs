using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Entities;
using Microsoft.AspNetCore.Identity;

namespace Backend.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResultDto> Register(RegisterDto registration);
        Task<AuthenticationResultDto> LogIn(LoginDto login);
        Task LogOut();
        Task CreateRoles();
        Task<IdentityResult> ChangePassword(AppUser user, string currentPassword, string newPassword);
        Task<IdentityResult> ForceChangePassword(AppUser user, string newPassword);
    }
}
