using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    // Group of transferred courses

    /// <summary>A class for representing a group of courses that have been transferred from another institution..</summary>
    public class TransferredCourseGroup
    {
        //[ForeignKey("CTEForm")]
        //public Guid CTEFormId { get; set; }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ICollection<TransferredCourse> TransferredCourses { get; set; }
        [Required]
        public ExemptedCourse ExemptedCourse { get; set; }
    }
}
