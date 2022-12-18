using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    /// <summary>A data transfer object for an app user.</summary>
    public class AppUserDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
