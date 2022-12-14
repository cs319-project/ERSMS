using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class AnnouncementDto
    {
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public string Title { get; set; }
        //[Required]
        public string Description { get; set; } = "Default description";
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
