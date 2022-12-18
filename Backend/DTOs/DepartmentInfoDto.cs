using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    /// <summary>A data transfer object for a department.</summary>
    public class DepartmentInfoDto
    {
        public string FacultyName { get; set; } = "Not Specified";
        public string DepartmentName { get; set; } = "Not Specified";
    }
}
