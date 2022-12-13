using Backend.Entities;
using Microsoft.AspNetCore.Identity;

namespace Backend.Utilities
{
    using BCrypt.Net;

    public class BCryptPasswordHasher : IPasswordHasher<AppUser>
    {
        public string HashPassword(AppUser user, string password)
        {
            return BCrypt.HashPassword(password, 12);
        }

        public PasswordVerificationResult VerifyHashedPassword(AppUser user, string hashedPassword, string givenPassword)
        {
            return BCrypt.Verify(givenPassword, hashedPassword) ?
                PasswordVerificationResult.Success :
                PasswordVerificationResult.Failed;
        }
    }
}
