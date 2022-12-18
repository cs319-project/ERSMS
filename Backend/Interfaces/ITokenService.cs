using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Services
{
    /// <summary>An interface for the JWT token service.</summary>
    public interface ITokenService
    {
        Task<String> CreateToken(AppUser user);
    }
}
