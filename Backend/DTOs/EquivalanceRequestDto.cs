using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class EquivalenceRequestDto
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HostUniversityName { get; set; }
        public string HostCourseName { get; set; }
        public string HostCourseCode { get; set; }
        public double HostCourseECTS { get; set; }
        public string FileName { get; set; }
        //public byte[] Syllabus { get; set; }
        public ExemptedCourseDto ExemptedCourse { get; set; }
        public ApprovalDto InstructorApproval { get; set; }
        public string AdditionalNotes { get; set; } = "";
        public bool IsCanceled { get; set; } = false;
        public bool IsRejected { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public bool IsArchived { get; set; } = false;
    }
}
