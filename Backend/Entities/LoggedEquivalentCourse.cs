using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    // Accepted equivalence course pairs
    public class LoggedEquivalentCourse : LoggedCourse
    {
        public ExemptedCourse ExemptedCourse { get; set; }
        public string HostCourseCode { get; set; }
        public string HostCourseName { get; set; }
        public double HostCourseECTS { get; set; }
    }
}
