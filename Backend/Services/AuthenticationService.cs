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
using Backend.Utilities;
using Backend.Utilities.Enum;

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

                switch (actorType)
                {
                    case "Exchange Coordinator":
                        var t = (user.DomainUser as ExchangeCoordinator);
                        t.Department = new DepartmentInfo();
                        t.Department.DepartmentName = EnumStringify.DepartmentEnumarator(register.Department.DepartmentName);
                        // (user.DomainUser as ExchangeCoordinator).Department.FacultyName = EnumStringify.FacultyEnumarator(register.Department.FacultyName);
                        var x = String.Compare(register.Department.FacultyName, "Engineering");
                        break;
                    default:
                        throw new System.ArgumentException("Invalid actor type");
                }

                if (register is CoordinatorRegisterDto)
                {
                    var department = EnumStringify.DepartmentEnumarator("CS");
                    if (user.DomainUser.GetType() == typeof(ExchangeCoordinator))
                    {
                        (user.DomainUser as ExchangeCoordinator).Department.DepartmentName = department;
                    }
                }

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
            var roles = EnumStringify.IdentityRoleList();
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new Role { Name = role });
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
