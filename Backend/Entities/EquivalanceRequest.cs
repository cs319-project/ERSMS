using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    public class EquivalanceRequest
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string StudentId { get; set; }
        public byte[] Syllabus { get; set; }
        public ExemptedCourse ExemptedCourse { get; set; }
        public Approval InstructorApproval { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
