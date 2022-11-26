using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    public class RequestedCourse
    {
        [ForeignKey("RequestedCourseGroup")]
        public Guid RequestedCourseGroupId { get; set; }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CourseCode { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public int Credits { get; set; }
    }
}
