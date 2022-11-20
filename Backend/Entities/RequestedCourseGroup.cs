using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    public class RequestedCourseGroup
    {
        [ForeignKey("PreApprovalForm")]
        public Guid PreApprovalFormId { get; set; }
        [Key]
        public Guid Id { get; set; }

        public ICollection<RequestedCourse> RequestedCourses { get; set; }
        public RequestedExemptedCourse RequestedExemptedCourse { get; set; }
    }
}
