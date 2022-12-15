using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Utilities.Enum;

namespace Backend.Entities
{
    public abstract class LoggedCourse
    {
        [Key]
        public Guid Id { get; set; }
        public string HostCourseCode { get; set; }
        public string HostCourseName { get; set; }
        public double HostCourseECTS { get; set; }
        public string BilkentCourseCode { get; set; }
        public string BilkentCourseName { get; set; }
        public double BilkentCourseECTS { get; set; } = 0.0;
        public int BilkentCourseCredits { get; set; }
        public CourseTypes BilkentCourseType { get; set; }
    }
}
