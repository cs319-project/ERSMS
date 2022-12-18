using Backend.Entities;
using Backend.Entities.Exceptions;
using Backend.Interfaces;
using Backend.Utilities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    /// <summary>A repository for To-Do items.</summary>
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly DataContext _context;

        /// <summary>Initializes a new instance of the <see cref="ToDoItemRepository"/> class.</summary>
        /// <param name="context">The data context.</param>
        public ToDoItemRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>Adds a to-do item to the current user's to-do list.</summary>
        /// <param name="userName">The user name of the current user.</param>
        /// <param name="toDoItem">The to-do item to add.</param>
        /// <returns>Whether the to-do item was added successfully.</returns>
        public async Task<bool> AddToDoItem(string userName, ToDoItem toDoItem)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.IdentityUser.UserName == userName);
            var coordinator = await _context.ExchangeCoordinators.FirstOrDefaultAsync(x => x.IdentityUser.UserName == userName);

            if (student == null && coordinator != null)
            {
                coordinator.ToDoList.Add(toDoItem);
            }
            else if (student != null)
            {
                student.ToDoList.Add(toDoItem);
            }
            else
            {
                throw new ToDoListException("No user found to add to do item to");
            }

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Adds a to do item to all coordinators in a department.</summary>
        /// <param name="toDoItem">The to do item to add.</param>
        /// <param name="department">The department to add the to do item to.</param>
        /// <returns>True if the to do item was added to all coordinators in the department; otherwise, false.</returns>
        public async Task<bool> AddToDoItemToAllByDepartment(ToDoItem toDoItem, Department department)
        {
            List<ExchangeCoordinator> coordinators = _context.ExchangeCoordinators.ToList();

            if (coordinators.Count == 0)
            {
                throw new ToDoListException("No coordinators found to add to do item to");
            }

            foreach (var coordinator in coordinators)
            {
                if (coordinator.Department.DepartmentName == department)
                {
                    if (coordinator.ToDoList == null)
                    {
                        coordinator.ToDoList = new List<ToDoItem>();
                    }

                    coordinator.ToDoList.Add(toDoItem);
                }
            }

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Changes the complete status of a ToDoItem.</summary>
        /// <param name="id">The ID of the ToDoItem.</param>
        /// <param name="isComplete">The new complete status.</param>
        /// <returns>True if the complete status was successfully changed; otherwise, false.</returns>
        public async Task<bool> ChangeCompleteToDoItem(Guid id, bool isComplete)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            item.IsComplete = isComplete;
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Deletes a ToDoItem from the database.</summary>
        /// <param name="id">The ID of the ToDoItem to delete.</param>
        /// <returns>True if the item was deleted, false otherwise.</returns>
        public async Task<bool> DeleteToDoItem(Guid id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            _context.ToDoItems.Remove(toDoItem);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets a ToDoItem from the database.</summary>
        /// <param name="id">The ID of the ToDoItem.</param>
        /// <returns>The ToDoItem.</returns>
        public async Task<ToDoItem> GetToDoItem(Guid id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            return toDoItem;
        }

        /// <summary>Gets a ToDoItem by its CascadeId.</summary>
        /// <param name="cascadeId">The CascadeId of the ToDoItem.</param>
        /// <returns>The ToDoItem with the specified CascadeId.</returns>
        public Task<ToDoItem> GetToDoItemByCascadeId(Guid cascadeId)
        {
            var toDoItem = _context.ToDoItems.FirstOrDefault(x => x.CascadeId == cascadeId);
            return Task.FromResult(toDoItem);
        }

        /// <summary>Gets the ToDoItems from the local database.</summary>
        /// <returns>The ToDoItems from the local database.</returns>
        public async Task<IEnumerable<ToDoItem>> GetToDoItems()
        {
            var toDoItems = await _context.ToDoItems.ToListAsync();
            return toDoItems;
        }

        /// <summary>Changes the star status of a ToDoItem.</summary>
        /// <param name="id">The ID of the ToDoItem.</param>
        /// <param name="isStarred">The new star status.</param>
        /// <returns>True if the star status was changed, false otherwise.</returns>
        public async Task<bool> ChangeStarToDoItem(Guid id, bool isStarred)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            item.IsStarred = isStarred;
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Updates the specified ToDoItem in the database.</summary>
        /// <param name="toDoItem">The ToDoItem to update.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public async Task<bool> UpdateToDoItem(ToDoItem toDoItem)
        {
            var item = _context.ToDoItems.Update(toDoItem);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
