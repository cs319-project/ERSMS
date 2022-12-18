using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    /// <summary>A class for representing a requested course group.</summary>
    public class RequestedCourseGroup
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ICollection<RequestedCourse> RequestedCourses { get; set; }
        [Required]
        public ExemptedCourse RequestedExemptedCourse { get; set; }
    }
}
