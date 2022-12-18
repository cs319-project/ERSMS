using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    /// <summary>A class for representing a message sent between users.</summary>
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.Now;

        // User deletion mechanism
        // public bool SenderDeleted { get; set; }
        // public bool RecipientDeleted { get; set; }
    }
}
