using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class CTEFormDto
    {
        public Guid Id { get; set; }

        public string SubjectStudentUserName { get; set; }

        public ICollection<TransferredCourseGroupDto> TransferredCourseGroups { get; set; }

        public DateTime SubmissionTime { get; set; }

        public DateTime ApprovalTime { get; set; }

        public ApprovalDto ChairApproval { get; set; }
        public ApprovalDto DeanApproval { get; set; }
        public ApprovalDto ExchangeCoordinatorApproval { get; set; }
    }
}
