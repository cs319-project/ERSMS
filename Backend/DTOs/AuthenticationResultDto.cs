using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class AuthenticationResultDto
    {
        public string UserName { get; set; }
        public Object UserDetails { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
