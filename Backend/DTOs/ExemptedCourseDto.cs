using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class ExemptedCourseDto
    {
        public Guid Id { get; set; }

        public int BilkentCredits { get; set; }
        public double ECTS { get; set; } = 0.0;
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string CourseType { get; set; }
    }
}
