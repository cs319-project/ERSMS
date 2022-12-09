using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces
{
    public interface IToDoItemService
    {
        Task<bool> AddToDoItem(string userName, ToDoItemDto toDoItem);
        Task<IEnumerable<ToDoItemDto>> GetToDoItems();
        Task<bool> DeleteToDoItem(Guid id);
        Task<ToDoItemDto> GetToDoItem(Guid id);
        Task<bool> UpdateToDoItem(ToDoItemDto toDoItem);
        Task<bool> ChangeCompleteToDoItem(Guid id, bool isComplete);
        Task<bool> AddToDoItemToAll(ToDoItemDto toDoItem);
        Task<ToDoItemDto> GetToDoItemByCascadeId(Guid cascadeId);
        Task<bool> DeleteToDoItemByCascadeId(Guid cascadeId);
        Task<bool> ChangeStarToDoItem(Guid id, bool isStarred);
    }
}
