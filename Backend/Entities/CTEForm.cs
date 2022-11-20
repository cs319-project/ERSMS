using Backend.Entities.Enums;

namespace Backend.Entities
{
    public class CTEForm
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IDNumber { get; set; }
        public string Department { get; set; }
        public string HostUniversityName { get; set; }
        public StudentType StudentType { get; set; }
        public ICollection<TransferredCourse> TransferredCourses { get; set; }

        // These following approvals can be turned into collection
        public Approval ChairApproval { get; set; }
        public Approval DeanApproval { get; set; }
        public Approval ExchangeCoordinatorApproval { get; set; }
    }
}
