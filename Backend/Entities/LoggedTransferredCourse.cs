namespace Backend.Entities
{
    // Successfully transfered course pairs

    /// <summary>A course that has been transferred before.</summary>
    public class LoggedTransferredCourse : LoggedCourse
    {
        public ICollection<TransferredCourseGroup> TransferredCourseGroups { get; set; }
    }
}
