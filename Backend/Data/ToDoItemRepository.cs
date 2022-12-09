using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Entities.Exceptions;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly DataContext _context;

        public ToDoItemRepository(DataContext context)
        {
            _context = context;
        }

        // Methods
        public async Task<bool> AddToDoItem(string userName, ToDoItem toDoItem)
        {
            var coordinator = _context.ExchangeCoordinators.FirstOrDefault(x => x.IdentityUser.UserName == userName);

            if (coordinator != null && coordinator.ToDoList == null)
            {
                coordinator.ToDoList = new List<ToDoItem>();
            }
            else if (coordinator == null)
            {
                return false;
            }

            coordinator.ToDoList.Add(toDoItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddToDoItemToAll(ToDoItem toDoItem)
        {
            List<ExchangeCoordinator> coordinators = _context.ExchangeCoordinators.ToList();

            if (coordinators.Count == 0)
            {
                throw new ToDoListException("No coordinators found to add to do item to");
            }

            foreach (var coordinator in coordinators)
            {
                if (coordinator.ToDoList == null)
                {
                    coordinator.ToDoList = new List<ToDoItem>();
                }

                coordinator.ToDoList.Add(toDoItem);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangeCompleteToDoItem(Guid id, bool isComplete)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            item.IsComplete = isComplete;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteToDoItem(Guid id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            _context.ToDoItems.Remove(toDoItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ToDoItem> GetToDoItem(Guid id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            return toDoItem;
        }

        public Task<ToDoItem> GetToDoItemByCascadeId(Guid cascadeId)
        {
            var toDoItem = _context.ToDoItems.FirstOrDefault(x => x.CascadeId == cascadeId);
            return Task.FromResult(toDoItem);
        }

        public async Task<IEnumerable<ToDoItem>> GetToDoItems()
        {
            var toDoItems = await _context.ToDoItems.ToListAsync();
            return toDoItems;
        }

        public async Task<bool> ChangeStarToDoItem(Guid id, bool isStarred)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            item.IsStarred = isStarred;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateToDoItem(ToDoItem toDoItem)
        {
            var item = _context.ToDoItems.Update(toDoItem);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
