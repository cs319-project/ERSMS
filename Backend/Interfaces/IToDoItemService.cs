using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Utilities.Enum;

namespace Backend.Interfaces
{
    /// <summary>An interface for the to-do item service.</summary>
    public interface IToDoItemService
    {
        Task<bool> AddToDoItem(string userName, ToDoItemDto toDoItem);
        Task<IEnumerable<ToDoItemDto>> GetToDoItems();
        Task<bool> DeleteToDoItem(Guid id);
        Task<ToDoItemDto> GetToDoItem(Guid id);
        Task<bool> UpdateToDoItem(ToDoItemDto toDoItem);
        Task<bool> ChangeCompleteToDoItem(Guid id, bool isComplete);
        Task<bool> AddToDoItemToAllByDepartment(ToDoItemDto toDoItem, Department department);
        Task<ToDoItemDto> GetToDoItemByCascadeId(Guid cascadeId);
        Task<bool> DeleteToDoItemByCascadeId(Guid cascadeId);
        Task<bool> ChangeStarToDoItem(Guid id, bool isStarred);
    }
}
