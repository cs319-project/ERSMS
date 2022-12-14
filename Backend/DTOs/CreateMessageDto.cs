using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class CreateMessageDto
    {
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
    }
}
