namespace Backend.DTOs
{
    /// <summary>A data transfer object for a to-do list item.</summary>
    public class ToDoItemDto
    {
        public Guid Id { get; set; }

        public Guid CascadeId { get; set; }

        //[Required]
        public string Title { get; set; }
        public string Description { get; set; }
        //[Required]
        public bool IsComplete { get; set; }
        //[Required]
        public bool IsStarred { get; set; }
    }
}
