namespace Backend.Entities
{
    // Accepted equivalence course pairs

    /// <summary>A class for representing a course that is equivalent to another course.</summary>
    public class LoggedEquivalentCourse : LoggedCourse
    {
        public ExemptedCourse ExemptedCourse { get; set; }
        public string HostCourseCode { get; set; }
        public string HostCourseName { get; set; }
        public double HostCourseECTS { get; set; }
    }
}
