using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class ExemptedCourseDto
    {
        public Guid Id { get; set; }

        public int Credits { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string CourseType { get; set; }
    }
}
