using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    /// <summary>A data transfer object for a logged transferred course.</summary>
    public class LoggedTransferredCourseDto : LoggedCourseDto
    {
        public ICollection<TransferredCourseGroupDto> TransferredCourseGroups { get; set; }
    }
}
