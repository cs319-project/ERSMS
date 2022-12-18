using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    /// <summary>A data transfer object for a department chair.</summary>
    /// <remarks>Dean is a boolean value that indicates whether the user is a dean.</remarks>
    public class DeanDepartmentChairDto : DomainUserDto
    {
        public DepartmentInfoDto Department { get; set; }
        public bool? IsDean { get; set; }
    }
}
