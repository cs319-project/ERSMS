using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public bool read { get; set; } = false;
        public string content { get; set; } = "";
        public Guid userId { get; set; }
    }
}
