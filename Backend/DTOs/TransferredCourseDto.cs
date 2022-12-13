using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class TransferredCourseDto
    {
        public Guid Id { get; set; }

        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public string Grade { get; set; }
        // Can be added for flagging the transfer status of the course
        //public Boolean IsPassed { get; set; }
    }
}