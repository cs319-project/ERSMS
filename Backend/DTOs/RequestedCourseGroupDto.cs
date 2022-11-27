using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class RequestedCourseGroupDto
    {
        public Guid Id { get; set; }

        public ICollection<RequestedCourseDto> RequestedCourses { get; set; }
        public RequestedExemptedCourseDto RequestedExemptedCourse { get; set; }
    }
}
