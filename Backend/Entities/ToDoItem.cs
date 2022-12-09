using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
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