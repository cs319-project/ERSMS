﻿using System.ComponentModel.DataAnnotations;
using Backend.Utilities.Enum;

namespace Backend.Entities
{
    /// <summary>A class for representing a pre-approval form.</summary>
    public class PreApprovalForm : Form
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
        public string AcademicYear { get; set; } // Data type can be changed
        [Required]
        public Semester Semester { get; set; }
        public byte[] PDF { get; set; }
        public string FileName { get; set; }

        // public Student SubjectStudent { get; set; }

        [Required]
        public DateTime SubmissionTime { get; set; } = DateTime.Now;

        public DateTime ApprovalTime { get; set; }

        public ICollection<RequestedCourseGroup> RequestedCourseGroups { get; set; }
        public Approval ExchangeCoordinatorApproval { get; set; }
        public Approval FacultyAdministrationBoardApproval { get; set; }
        public Guid ToDoItemId { get; set; }
        public bool IsCanceled { get; set; } = false;
        public bool IsRejected { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public bool IsArchived { get; set; } = false;
    }
}
