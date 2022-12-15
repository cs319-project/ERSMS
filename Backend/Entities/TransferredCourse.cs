using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    // Host university course for CTE form
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
