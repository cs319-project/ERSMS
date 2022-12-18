using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    /// <summary>A class for representing an course equivalence request.</summary>
    public class EquivalenceRequest : Form
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string StudentId { get; set; }
        public string HostCourseName { get; set; }
        public string HostCourseCode { get; set; }
        public double HostCourseECTS { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public string FileName { get; set; }
        public byte[] Syllabus { get; set; }
        public ExemptedCourse ExemptedCourse { get; set; }
        public Approval InstructorApproval { get; set; }
        public string AdditionalNotes { get; set; } = "";
        public bool IsCanceled { get; set; } = false;
        public bool IsRejected { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public bool IsArchived { get; set; } = false;
    }
}
