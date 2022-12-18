using Backend.Entities;

namespace Backend.Services
{
    /// <summary>An interface for the JWT token service.</summary>
    public interface ITokenService
    {
        Task<String> CreateToken(AppUser user);
    }
}
