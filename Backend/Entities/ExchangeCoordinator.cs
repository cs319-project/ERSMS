using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    /// <summary>A class for representing an exchange coordinator.</summary>
    [Table("ExchangeCoordinators")]
    public class ExchangeCoordinator : DomainUser
    {
        public DepartmentInfo Department { get; set; }

        public ICollection<ToDoItem> ToDoList { get; set; }
    }
}
