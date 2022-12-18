using Backend.Entities;
using Microsoft.AspNetCore.Identity;

namespace Backend.Utilities
{
    using BCrypt.Net;

    /// <summary>Implements a password hashing algorithm using the BCrypt library.</summary>
    /// <remarks>This implementation is based on the BCrypt.Net library.</remarks>
    public class BCryptPasswordHasher : IPasswordHasher<AppUser>
    {
        /// <summary>Hashes a password.</summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The hashed password.</returns>
        public string HashPassword(AppUser user, string password)
        {
            return BCrypt.HashPassword(password, 12);
        }

        /// <summary>Verifies a hashed password.</summary>
        /// <param name="user">The user.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <param name="givenPassword">The given password.</param>
        /// <returns>The result of the verification.</returns>
        public PasswordVerificationResult VerifyHashedPassword(AppUser user, string hashedPassword, string givenPassword)
        {
            return BCrypt.Verify(givenPassword, hashedPassword) ?
                PasswordVerificationResult.Success :
                PasswordVerificationResult.Failed;
        }
    }
}
