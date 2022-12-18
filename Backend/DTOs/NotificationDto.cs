namespace Backend.DTOs
{
    /// <summary>A data transfer object for a notification.</summary>
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public bool read { get; set; } = false;
        public string content { get; set; } = "";
        public Guid userId { get; set; }
    }
}
