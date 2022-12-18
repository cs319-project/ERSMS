using Backend.Entities;

namespace Backend.Interfaces
{
    /// <summary>An interface for the JWT token service.</summary>
    public interface ITokenService
    {
        Task<String> CreateToken(AppUser user);
    }
}
