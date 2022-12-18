using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    /// <summary>A data transfer object for a transferred course.</summary>
    public class TransferredCourseDto
    {
        public Guid Id { get; set; }

        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public double ECTS { get; set; }
        public string Grade { get; set; }
        // Can be added for flagging the transfer status of the course
        //public Boolean IsPassed { get; set; }
    }
}
