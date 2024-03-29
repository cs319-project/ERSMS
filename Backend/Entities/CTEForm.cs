﻿using System.ComponentModel.DataAnnotations;
using Backend.Utilities.Enum;

namespace Backend.Entities
{
    /// <summary>A class for representing a CTE (Course Transfer and Exemption) form.</summary>
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
        [Required]
        public ICollection<TransferredCourseGroup> TransferredCourseGroups { get; set; }
        [Required]
        public DateTime SubmissionTime { get; set; } = DateTime.Now;
        public DateTime ApprovalTime { get; set; }
        public byte[] PDF { get; set; }
        public string FileName { get; set; }

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
