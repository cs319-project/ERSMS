using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Backend.DataAnnotations;

namespace Backend.DTOs
{
    public class RegisterDto
    {
        [Required] public string ActorType { get; set; }

        [BilkentMailAttribute] public string Email { get; set; }

        [Required] public string UserName { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Password { get; set; }

        public DepartmentInfoDto Department { get; set; } = null!;
        [AllowNull] public bool? IsDean { get; set; } = null;
        [AllowNull] public bool? IsCourseCoordinator { get; set; } = null;
    }
}
