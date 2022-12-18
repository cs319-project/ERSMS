using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Utilities.Enum;

namespace Backend.Entities
{
    // This can be divided into inheritance relations as exemptedelective, exemptedgeneric rather than using enum and nullables
    // Course in Bilkent

    /// <summary>A class for representing an exempted course.</summary>
    public class ExemptedCourse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int BilkentCredits { get; set; }
        public double ECTS { get; set; } = 0.0;
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        [Required]
        public CourseTypes CourseType { get; set; }
    }
}
