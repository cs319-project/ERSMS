using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class CourseCoordinatorInstructorDto : DomainUserDto
    {
        public DepartmentInfoDto Department { get; set; }
        public CourseDto Course { get; set; } = null!;
        public bool? IsCourseCoordinator { get; set; } = null;
    }
}
