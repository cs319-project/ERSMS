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
        [UserNameAttribute] public string UserName { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Password { get; set; }
        //public IEnumerable<DepartmentInfoDto> Majors { get; set; } = null!; // NEWLY ADDED
        //public string ExchangeSchool { get; set; } = null!; // NEWLY ADDED
        public DepartmentInfoDto Department { get; set; } = new DepartmentInfoDto();
        public bool IsDean { get; set; } = false;
        public bool IsCourseCoordinator { get; set; } = false;
        public CourseDto Course { get; set; } = new CourseDto();
    }
}
