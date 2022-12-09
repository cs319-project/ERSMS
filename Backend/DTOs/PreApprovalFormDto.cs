using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class PreApprovalFormDto
    {
        public Guid Id { get; set; }

        // public string SubjectStudentUserName { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IDNumber { get; set; }

        public string Department { get; set; }

        public string HostUniversityName { get; set; }

        public string AcademicYear { get; set; } // Data type can be changed

        public string Semester { get; set; }

        public DateTime SubmissionTime { get; set; }

        public DateTime ApprovalTime { get; set; }

        public ICollection<RequestedCourseGroupDto> RequestedCourseGroups { get; set; }

        public ApprovalDto ExchangeCoordinatorApproval { get; set; }

        public ApprovalDto FacultyAdministrationBoardApproval { get; set; }
    }
}
