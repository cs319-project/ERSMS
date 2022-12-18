using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    /// <summary>A class representing an announcement.</summary>
    public class Announcement
    {
        [Key]
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public string Title { get; set; }
        [Required]
        public string Description { get; set; } = "Default description";
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
