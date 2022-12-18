using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    /// <summary>A data transfer object for a requested course.</summary>
    public class RequestedCourseGroupDto
    {
        public Guid Id { get; set; }

        public ICollection<RequestedCourseDto> RequestedCourses { get; set; }
        public ExemptedCourseDto RequestedExemptedCourse { get; set; }
    }
}
