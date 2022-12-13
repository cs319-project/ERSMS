using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    [Table("CourseCoordinatorInstructors")]
    public class CourseCoordinatorInstructor : DomainUser
    {
        public DepartmentInfo Department { get; set; }

        // TODO: Convert this to a CourseList
        public Course Course { get; set; }
        public bool? IsCourseCoordinator { get; set; }
    }
}
