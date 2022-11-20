using Backend.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    public class PreApprovalForm
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int IDNumber { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string HostUniversityName { get; set; }
        [Required]
        public string AcademicYear { get; set; } // Data type can be changed
        [Required]
        public Semesters Semester { get; set; }
        public ICollection<RequestedCourseGroup> RequestedCourseGroups { get; set; }
    }
}
