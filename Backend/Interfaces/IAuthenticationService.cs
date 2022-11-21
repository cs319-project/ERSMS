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
        Task<IdentityResult> Register(RegisterDto registration);
        Task<SignInResult> LogIn(LoginDto login);
        Task LogOut();
        Task CreateRoles();

        // Get user by id
        DomainUser GetUserById(string id);

    }
}
