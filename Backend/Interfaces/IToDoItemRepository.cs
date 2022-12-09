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
        Task<bool> CompleteToDoItem(Guid id);
        Task<bool> AddToDoItemToAll(ToDoItem toDoItem);
    }
}
