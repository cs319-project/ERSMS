using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    /// <summary>A data transfer object for a course.</summary>
    public class CourseDto
    {
        public string CourseCode { get; set; } = "";
        public string CourseName { get; set; } = "";
    }
}
