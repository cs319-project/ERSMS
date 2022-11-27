using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class PreApprovalFormDto
    {
        public Guid Id { get; set; }

        public string SubjectStudentUserName { get; set; }

        public DateTime SubmissionTime { get; set; }

        public DateTime ApprovalTime { get; set; }

        public ICollection<RequestedCourseGroupDto> RequestedCourseGroups { get; set; }
        public ApprovalDto ExchangeCoordinatorApproval { get; set; }
    }
}
