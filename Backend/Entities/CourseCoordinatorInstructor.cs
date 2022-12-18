using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    /// <summary>A class for a course coordinator or instructor of a course.</summary>
    /// <remarks>
    /// A course coordinator is a user who is responsible for a course.
    /// </remarks>
    [Table("CourseCoordinatorInstructors")]
    public class CourseCoordinatorInstructor : DomainUser
    {
        public DepartmentInfo Department { get; set; }

        // TODO: Convert this to a CourseList
        public Course Course { get; set; }
        public bool IsCourseCoordinator { get; set; } = false;
    }
}
