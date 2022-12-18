namespace Backend.DTOs
{
    /// <summary>A data transfer object for a message.</summary>
    public class MessageDto
    {

        public Guid Id { get; set; }
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
        public DateTime MessageSent { get; set; }
    }
}
