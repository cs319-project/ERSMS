﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    /// <summary>A class representing an approval.</summary>
    public class Approval
    {
        //[ForeignKey("CTEForm")]
        //public Guid CTEFormId { get; set; }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime DateOfApproval { get; set; } = DateTime.Now;
        public string Comment { get; set; } = "";
        [Required]
        public Boolean IsApproved { get; set; }
        // TODO think about the signature integration
    }
}
