using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Interfaces
{
    public interface IToDoItemRepository
    {
        Task<bool> AddToDoItem(string userName, ToDoItem toDoItem);
        Task<IEnumerable<ToDoItem>> GetToDoItems();
        Task<bool> DeleteToDoItem(Guid id);
        Task<ToDoItem> GetToDoItem(Guid id);
        Task<bool> UpdateToDoItem(ToDoItem toDoItem);
        Task<bool> ChangeCompleteToDoItem(Guid id, bool isComplete);
        Task<bool> ChangeStarToDoItem(Guid id, bool isStarred);
        Task<bool> AddToDoItemToAll(ToDoItem toDoItem);
        Task<ToDoItem> GetToDoItemByCascadeId(Guid cascadeId);
    }
}
