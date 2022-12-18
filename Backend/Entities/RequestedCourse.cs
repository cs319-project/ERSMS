using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    /// <summary>A class for representing a requested course.</summary>
    public class RequestedCourse
    {
        //[ForeignKey("RequestedCourseGroup")]
        //public Guid RequestedCourseGroupId { get; set; }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CourseCode { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public double ECTS { get; set; }
    }
}
