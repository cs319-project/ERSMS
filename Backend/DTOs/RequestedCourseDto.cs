namespace Backend.DTOs
{
    /// <summary>A data transfer object for a requested course.</summary>
    public class RequestedCourseDto
    {
        public Guid Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public double ECTS { get; set; }
    }
}
