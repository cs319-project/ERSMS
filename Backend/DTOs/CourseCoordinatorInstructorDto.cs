namespace Backend.DTOs
{
    /// <summary>A data transfer object for a course coordinator.</summary>
    public class CourseCoordinatorInstructorDto : DomainUserDto
    {
        public DepartmentInfoDto Department { get; set; }
        public CourseDto Course { get; set; } = null!;
        public bool? IsCourseCoordinator { get; set; } = null;
    }
}
