using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Backend.Entities.Enums;

namespace Backend.Entities
{
    // Course that is going to be exempted in the Bilkent curriculum upon the forms approval
    public class RequestedExemptedCourse
    {
        [ForeignKey("RequestedCourseGroup")]
        public Guid RequestedCourseGroupId { get; set; }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Credits { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        [Required]
        public CourseTypesPreApproval CourseType { get; set; }
    }
}
