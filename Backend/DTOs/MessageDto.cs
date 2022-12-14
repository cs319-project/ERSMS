using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class MessageDto
    {

        public Guid Id { get; set; }
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
        public DateTime MessageSent { get; set; }
    }
}
