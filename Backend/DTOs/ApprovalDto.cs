using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class ApprovalDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfApproval { get; set; } = DateTime.Now;
        public Boolean IsApproved { get; set; } = false;
        public string Comment { get; set; } = "";
    }
}
