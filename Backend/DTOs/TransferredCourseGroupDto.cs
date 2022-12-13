using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class TransferredCourseGroupDto
    {
        public Guid Id { get; set; }
        public ICollection<TransferredCourseDto> TransferredCourses { get; set; }
        public ExemptedCourseDto ExemptedCourse { get; set; }
    }
}
