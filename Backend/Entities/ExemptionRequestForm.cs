using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
	public class ExemptionRequestForm
	{

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Student SubjectStudent { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTime SubmissionTime { get; set; }

        public DateTime ApprovalTime { get; set; }

        public string CourseCodeHost { get; set; }

        public string CourseCodeBilkent { get; set; }

        // TODO file upload
    }
}

