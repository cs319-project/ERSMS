using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    /// <summary>A data transfer object for a semester.</summary>
    public class SemesterInfoDto
    {
        public string AcademicYear { get; set; } = null;
        public string Semester { get; set; }
    }
}
