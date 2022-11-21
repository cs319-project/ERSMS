using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Backend.DTOs;
using Microsoft.AspNetCore.Identity;

using Backend.Utilities;

namespace Backend.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser() { }

        public AppUser(RegisterDto register, string actorType)
        {
            UserName = register.UserName;
            Email = register.Email;

            DomainUser = DomainUserFactory.Create(register);
        }

        public DomainUser DomainUser { get; set; } = null!;
    }
}
