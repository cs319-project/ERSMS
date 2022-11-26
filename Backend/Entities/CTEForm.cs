using Backend.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    public class CTEForm
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
        public StudentType StudentType { get; set; }
        public ICollection<TransferredCourseGroup> TransferredCourseGroups { get; set; }

        // These following approvals can be turned into collection
        public Approval ChairApproval { get; set; }
        public Approval DeanApproval { get; set; }
        public Approval ExchangeCoordinatorApproval { get; set; }
    }
}
