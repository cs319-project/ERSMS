using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    [Table("DeanDepartmentChairs")]
    public class DeanDepartmentChair : DomainUser
    {
        public DepartmentInfo Department { get; set; }
        public bool IsDean { get; set; } = false;
    }
}
