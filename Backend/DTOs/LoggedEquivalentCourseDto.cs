using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class LoggedEquivalentCourseDto : LoggedCourseDto
    {
        public ExemptedCourseDto ExemptedCourse { get; set; }
        public string HostCourseCode { get; set; }
        public string HostCourseName { get; set; }
        public double HostCourseECTS { get; set; }
    }
}
