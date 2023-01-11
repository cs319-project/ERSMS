namespace Backend.DTOs
{
    /// <summary>A data transfer object for a logged course.</summary>
    public abstract class LoggedCourseDto
    {
        public Guid Id { get; set; }
        public DateTime LogTime { get; set; } = DateTime.Now;
        // public string HostCourseCode { get; set; }
        // public string HostCourseName { get; set; }
        // public double HostCourseECTS { get; set; }
        // public string BilkentCourseCode { get; set; }
        // public string BilkentCourseName { get; set; }
        // public double BilkentCourseECTS { get; set; } = 0.0;
        // public int BilkentCourseCredits { get; set; }
        // public string BilkentCourseType { get; set; }
    }
}
