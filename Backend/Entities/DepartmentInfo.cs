using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Utilities.Enum;

namespace Backend.Entities
{
    public struct DepartmentInfo
    {
        public Department DepartmentName { get; set; }
        public Faculty FacultyName { get; set; }
    }
}
