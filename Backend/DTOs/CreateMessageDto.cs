namespace Backend.DTOs
{
    /// <summary>A data transfer object for creating a message.</summary>
    public class CreateMessageDto
    {
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
    }
}
