using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    // Host university course for CTE form

    /// <summary>A class for representing a course that has been transferred from another institution.</summary>
    public class TransferredCourse
    {
        //[ForeignKey("TransferredCourseGroup")]
        //public Guid TransferredCourseGroupId { get; set; }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CourseCode { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public double ECTS { get; set; }
        [Required]
        public string Grade { get; set; }
        // Can be added for flagging the transfer status of the course
        //public Boolean IsPassed { get; set; }
    }
}
