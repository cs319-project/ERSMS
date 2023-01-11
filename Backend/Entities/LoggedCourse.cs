using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    /// <summary>A class for representing a logged (archived) course.</summary>
    public abstract class LoggedCourse
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime LogTime { get; set; } = DateTime.Now;
        // public string HostCourseCode { get; set; }
        // public string HostCourseName { get; set; }
        // public double HostCourseECTS { get; set; }
        // public string BilkentCourseCode { get; set; }
        // public string BilkentCourseName { get; set; }
        // public double BilkentCourseECTS { get; set; } = 0.0;
        // public int BilkentCourseCredits { get; set; }
        // public CourseTypes BilkentCourseType { get; set; }
    }
}
