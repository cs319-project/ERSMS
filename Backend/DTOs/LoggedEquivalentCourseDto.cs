namespace Backend.DTOs
{
    /// <summary>A data transfer object for a logged equivalent course.</summary>
    public class LoggedEquivalentCourseDto : LoggedCourseDto
    {
        public ExemptedCourseDto ExemptedCourse { get; set; }
        public string HostCourseCode { get; set; }
        public string HostCourseName { get; set; }
        public double HostCourseECTS { get; set; }
        public string HostSchool { get; set; }
    }
}
