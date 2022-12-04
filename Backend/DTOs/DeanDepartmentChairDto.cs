using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class DeanDepartmentChairDto : DomainUserDto
    {
        public DepartmentInfoDto Department { get; set; }
        public bool? IsDean { get; set; }
    }
}
