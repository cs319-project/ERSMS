using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
