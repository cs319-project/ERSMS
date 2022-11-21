using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Entities;
using Backend.Interfaces;
using Backend.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services
{
    public class AuthenticationService : DataContextService, IAuthenticationService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthenticationService(
            DataContext dataContext,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<Role> roleManager
        ) : base(dataContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Register(RegisterDto register)
        {
            try
            {
                string actorType = register.ActorType;
                AppUser user = new AppUser(register, actorType);

                var result = await _userManager.CreateAsync(user, register.Password);
                return result.Succeeded ? await AssignRole(user, actorType) : result;
            }
            catch (System.ArgumentException e)
            {
                return IdentityResult.Failed(new IdentityError { Description = e.Message });
            }
        }

        public Task<IdentityResult> AssignRole(AppUser user, string actorType)
        {
            return _userManager.AddToRoleAsync(user, actorType);
        }

        public async Task<SignInResult> LogIn(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user is null) return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync(user, login.Password, isPersistent: false, lockoutOnFailure: false);
        }

        Task IAuthenticationService.LogOut()
        {
            throw new NotImplementedException();
        }

        public async Task CreateRoles()
        {
            var values = Enum.GetNames(typeof(Backend.Utilities.Enum.Actors));
            foreach (var actor in values)
            {
                if (!await _roleManager.RoleExistsAsync(actor))
                {
                    await _roleManager.CreateAsync(new Role { Name = actor });
                }
            }
        }

        public DomainUser GetUserById(string id)
        {
            var user = _dataContext.DomainUsers.SingleOrDefault(x => x.IdentityUser.UserName == id);
            return user;
        }
    }
}
