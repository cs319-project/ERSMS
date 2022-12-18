namespace Backend.DTOs
{
    /// <summary>A data transfer object for a transferred course group.</summary>
    public class TransferredCourseGroupDto
    {
        public Guid Id { get; set; }
        public ICollection<TransferredCourseDto> TransferredCourses { get; set; }
        public ExemptedCourseDto ExemptedCourse { get; set; }
    }
}
