using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    // Accepted equivalance course pairs
    public class LoggedEquivalantCourse : LoggedCourse
    {
        public ExemptedCourse ExemptedCourse { get; set; }
        public string HostCourseCode { get; set; }
        public string HostCourseName { get; set; }
        public double HostCourseECTS { get; set; }
    }
}
