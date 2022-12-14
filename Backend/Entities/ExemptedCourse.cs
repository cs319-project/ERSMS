using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Utilities.Enum;

namespace Backend.Entities
{
    // This can be divided into inheritance relations as exemptedelective, exemptedgeneric rather than using enum and nullables
    public class ExemptedCourse
    {
        //[ForeignKey("TransferredCourse")]
        //public Guid TransferredCourseId { get; set; }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Credits { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        [Required]
        public CourseTypes CourseType { get; set; }
    }
}
