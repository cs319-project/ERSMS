using Backend.DTOs;
using Backend.Utilities;
using Microsoft.AspNetCore.Identity;

namespace Backend.Entities
{
    /// <summary>A user of the application.</summary>
    /// <remarks>
    /// This class is used to represent a user of the application.
    /// </remarks>
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
