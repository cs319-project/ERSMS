using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class ExchangeCoordinatorDto : DomainUserDto
    {
        public DepartmentInfoDto Department { get; set; }

        public ICollection<ToDoItemDto> ToDoList { get; set; }
    }
}
