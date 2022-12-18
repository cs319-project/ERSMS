using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    /// <summary>A class for representing a to-do item.</summary>
    public class ToDoItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CascadeId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsComplete { get; set; } = false;
        [Required]
        public bool IsStarred { get; set; } = false;
    }
}
