using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
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
