using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Utilities.Enum;

namespace Backend.Interfaces
{
    public interface IToDoItemRepository
    {
        Task<bool> AddToDoItem(DepartmentInfo departmentInfo, ToDoItem toDoItem);
        Task<IEnumerable<ToDoItem>> GetToDoItems();
        Task<bool> DeleteToDoItem(Guid id);
        Task<ToDoItem> GetToDoItem(Guid id);
        Task<bool> UpdateToDoItem(ToDoItem toDoItem);
        Task<bool> ChangeCompleteToDoItem(Guid id, bool isComplete);
        Task<bool> ChangeStarToDoItem(Guid id, bool isStarred);
        Task<bool> AddToDoItemToAllByDepartment(ToDoItem toDoItem, Department department);
        Task<ToDoItem> GetToDoItemByCascadeId(Guid cascadeId);
    }
}
