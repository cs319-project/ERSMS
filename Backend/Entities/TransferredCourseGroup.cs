using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Entities
{
    // Group of transferred courses
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
