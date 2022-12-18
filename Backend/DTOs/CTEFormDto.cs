namespace Backend.DTOs
{
    /// <summary>A data transfer object for a CTE Form.</summary>
    public class CTEFormDto
    {
        public Guid Id { get; set; }

        //public string SubjectStudentUserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IDNumber { get; set; }
        public string Department { get; set; }
        public string HostUniversityName { get; set; }

        public ICollection<TransferredCourseGroupDto> TransferredCourseGroups { get; set; }

        public DateTime SubmissionTime { get; set; } = DateTime.Now;
        public string FileName { get; set; }

        public DateTime ApprovalTime { get; set; }

        public ApprovalDto ChairApproval { get; set; }
        public ApprovalDto DeanApproval { get; set; }
        public ApprovalDto ExchangeCoordinatorApproval { get; set; }
        public ApprovalDto FacultyOfAdministrationBoardApproval { get; set; }

        public bool IsCanceled { get; set; } = false;
        public bool IsRejected { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public bool IsArchived { get; set; } = false;
    }
}
