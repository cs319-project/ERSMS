using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Utilities.Enum;

namespace Backend.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IMapper _mapper;
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IUserRepository _userRepository;

        // Constructor
        public ToDoItemService(IMapper mapper, IToDoItemRepository toDoItemRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _toDoItemRepository = toDoItemRepository;
            _userRepository = userRepository;
        }

        // Methods
        public async Task<bool> AddToDoItem(string userName, ToDoItemDto toDoItem)
        {

            var exchangeCoordinator = _userRepository.GetExchangeCoordinatorByUserName(userName).Result;

            if (exchangeCoordinator == null)
            {
                return false;
            }

            ToDoItem itemEntity = _mapper.Map<ToDoItem>(toDoItem);
            DepartmentInfo departmentEntity = _mapper.Map<DepartmentInfo>(exchangeCoordinator.Department);
            return await _toDoItemRepository.AddToDoItem(departmentEntity, itemEntity);
        }

        public async Task<bool> AddToDoItemToAllByDepartment(ToDoItemDto toDoItem, Department department)
        {
            ToDoItem itemEntity = _mapper.Map<ToDoItem>(toDoItem);
            return await _toDoItemRepository.AddToDoItemToAllByDepartment(itemEntity, department);
        }

        public Task<bool> ChangeStarToDoItem(Guid id, bool isStarred)
        {
            return _toDoItemRepository.ChangeStarToDoItem(id, isStarred);
        }

        public async Task<bool> ChangeCompleteToDoItem(Guid id, bool isComplete)
        {
            return await _toDoItemRepository.ChangeCompleteToDoItem(id, isComplete);
        }

        public async Task<bool> DeleteToDoItem(Guid id)
        {
            return await _toDoItemRepository.DeleteToDoItem(id);
        }

        public async Task<bool> DeleteToDoItemByCascadeId(Guid cascadeId)
        {
            var item = await _toDoItemRepository.GetToDoItemByCascadeId(cascadeId);
            return await _toDoItemRepository.DeleteToDoItem(item.Id);
        }

        public async Task<ToDoItemDto> GetToDoItem(Guid id)
        {
            ToDoItem item = await _toDoItemRepository.GetToDoItem(id);
            return _mapper.Map<ToDoItemDto>(item);
        }

        public async Task<ToDoItemDto> GetToDoItemByCascadeId(Guid cascadeId)
        {
            ToDoItem item = await _toDoItemRepository.GetToDoItemByCascadeId(cascadeId);
            return _mapper.Map<ToDoItemDto>(item);
        }

        public async Task<IEnumerable<ToDoItemDto>> GetToDoItems()
        {
            IEnumerable<ToDoItem> items = await _toDoItemRepository.GetToDoItems();
            return _mapper.Map<IEnumerable<ToDoItemDto>>(items);
        }

        public async Task<bool> UpdateToDoItem(ToDoItemDto toDoItem)
        {
            ToDoItem item = _mapper.Map<ToDoItem>(toDoItem);
            return await _toDoItemRepository.UpdateToDoItem(item);
        }
    }
}
