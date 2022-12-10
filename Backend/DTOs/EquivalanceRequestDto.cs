using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class EquivalanceRequestDto
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public byte[] Syllabus { get; set; }
        public ExemptedCourseDto ExemptedCourse { get; set; }
        public ApprovalDto InstructorApproval { get; set; }
    }
}
