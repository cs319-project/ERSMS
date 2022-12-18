using System.ComponentModel.DataAnnotations;

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
