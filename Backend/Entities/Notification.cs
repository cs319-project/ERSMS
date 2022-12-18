using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    /// <summary>A class for representing a notification.</summary>
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }
        public bool read { get; set; } = false;
        [Required]
        public string content { get; set; } = "";
        [Required]
        public Guid userId { get; set; }
    }
}
