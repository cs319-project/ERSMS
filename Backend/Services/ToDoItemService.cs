using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Utilities.Enum;

namespace Backend.Services
{
    /// <summary>A service for to-do item operations.</summary>
    public class ToDoItemService : IToDoItemService
    {
        private readonly IMapper _mapper;
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>Creates a new instance of the <see cref="ToDoItemService"/> class.</summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="toDoItemRepository">The to do item repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public ToDoItemService(IMapper mapper, IToDoItemRepository toDoItemRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _toDoItemRepository = toDoItemRepository;
            _userRepository = userRepository;
        }

        /// <summary>Adds a new to-do item to the database.</summary>
        /// <param name="userName">The username.</param>
        /// <param name="toDoItem">The to-do item to add.</param>
        /// <returns>Whether the operation was successful.</returns>
        public async Task<bool> AddToDoItem(string userName, ToDoItemDto toDoItem)
        {
            ToDoItem itemEntity = _mapper.Map<ToDoItem>(toDoItem);
            return await _toDoItemRepository.AddToDoItem(userName, itemEntity);
        }

        /// <summary>Adds a to-do item to all users in a department.</summary>
        /// <param name="toDoItem">The to-do item to add.</param>
        /// <param name="department">The department to add the to-do item to.</param>
        /// <returns>Whether the operation was successful.</returns>
        public async Task<bool> AddToDoItemToAllByDepartment(ToDoItemDto toDoItem, Department department)
        {
            ToDoItem itemEntity = _mapper.Map<ToDoItem>(toDoItem);
            return await _toDoItemRepository.AddToDoItemToAllByDepartment(itemEntity, department);
        }

        /// <summary>Changes the star status of a to-do item.</summary>
        /// <param name="id">The ID of the to-do item.</param>
        /// <param name="isStarred">The new star status.</param>
        /// <returns>A task that completes when the change is complete.</returns>
        public Task<bool> ChangeStarToDoItem(Guid id, bool isStarred)
        {
            return _toDoItemRepository.ChangeStarToDoItem(id, isStarred);
        }

        /// <summary>Changes the complete status of a to-do item.</summary>
        /// <param name="id">The ID of the to-do item.</param>
        /// <param name="isComplete">The new complete status.</param>
        /// <returns>True if the to-do item was changed, false otherwise.</returns>
        public async Task<bool> ChangeCompleteToDoItem(Guid id, bool isComplete)
        {
            return await _toDoItemRepository.ChangeCompleteToDoItem(id, isComplete);
        }

        /// <summary>Deletes a to-do item from the repository.</summary>
        /// <param name="id">The ID of the to-do item to delete.</param>
        /// <returns>A task that completes when the item has been deleted.</returns>
        public async Task<bool> DeleteToDoItem(Guid id)
        {
            return await _toDoItemRepository.DeleteToDoItem(id);
        }

        /// <summary>Deletes a to-do item by cascade ID.</summary>
        /// <param name="cascadeId">The cascade ID of the to-do item to delete.</param>
        /// <returns>A task that completes when the to-do item is deleted.</returns>
        public async Task<bool> DeleteToDoItemByCascadeId(Guid cascadeId)
        {
            var item = await _toDoItemRepository.GetToDoItemByCascadeId(cascadeId);
            return await _toDoItemRepository.DeleteToDoItem(item.Id);
        }

        /// <summary>Gets a to-do item.</summary>
        /// <param name="id">The ID of the ToDoItem.</param>
        /// <returns>The to-do item.</returns>
        public async Task<ToDoItemDto> GetToDoItem(Guid id)
        {
            ToDoItem item = await _toDoItemRepository.GetToDoItem(id);
            return _mapper.Map<ToDoItemDto>(item);
        }

        /// <summary>Gets a to-do item by its cascade ID.</summary>
        /// <param name="cascadeId">The cascade ID of the to-do item.</param>
        /// <returns>The to-do item.</returns>
        public async Task<ToDoItemDto> GetToDoItemByCascadeId(Guid cascadeId)
        {
            ToDoItem item = await _toDoItemRepository.GetToDoItemByCascadeId(cascadeId);
            return _mapper.Map<ToDoItemDto>(item);
        }

        /// <summary>Gets the to-do items.</summary>
        /// <returns>The to-do items.</returns>
        public async Task<IEnumerable<ToDoItemDto>> GetToDoItems()
        {
            IEnumerable<ToDoItem> items = await _toDoItemRepository.GetToDoItems();
            return _mapper.Map<IEnumerable<ToDoItemDto>>(items);
        }

        /// <summary>Updates the specified to-do item.</summary>
        /// <param name="toDoItem">The to-do item to update.</param>
        /// <returns>A task that represents the operation.</returns>
        public async Task<bool> UpdateToDoItem(ToDoItemDto toDoItem)
        {
            ToDoItem item = _mapper.Map<ToDoItem>(toDoItem);
            return await _toDoItemRepository.UpdateToDoItem(item);
        }
    }
}
