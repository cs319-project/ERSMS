using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class LoggedTransferredCourseDto : LoggedCourseDto
    {
        public ICollection<TransferredCourseGroupDto> TransferredCourseGroups { get; set; }
    }
}
