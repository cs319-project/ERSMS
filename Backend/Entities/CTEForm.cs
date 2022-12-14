using Backend.Utilities.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    public class CTEForm : Form
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string IDNumber { get; set; }
        [Required]
        public Department Department { get; set; }
        [Required]
        public string HostUniversityName { get; set; }

        // [Required]
        // public Student SubjectStudent { get; set; }

        [Required]
        public ICollection<TransferredCourseGroup> TransferredCourseGroups { get; set; }

        [Required]
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        // [DefaultValue("getutcdate()")]
        public DateTime SubmissionTime { get; set; }

        public DateTime ApprovalTime { get; set; }

        // These following approvals can be turned into collection
        public Approval ChairApproval { get; set; }
        public Approval DeanApproval { get; set; }
        public Approval ExchangeCoordinatorApproval { get; set; }

        // Faculty of Administration Board Decision
        public Approval FacultyOfAdministrationBoardApproval { get; set; }

        public Guid ToDoItemId { get; set; }

        /*
         * IsCanceled = canceled
         * IsRejected = rejected
         * IsApproved = approved
         * IsArchived = archived
         * otherwise = pending
         */
        public bool IsCanceled { get; set; } = false;
        public bool IsRejected { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public bool IsArchived { get; set; } = false;
    }
}
