using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Services
{
    public interface ITokenService
    {
        Task<String> CreateToken(AppUser user);
    }
}
