using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;

namespace Backend.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IMapper _mapper;
        private readonly IToDoItemRepository _toDoItemRepository;

        // Constructor
        public ToDoItemService(IMapper mapper, IToDoItemRepository toDoItemRepository)
        {
            _mapper = mapper;
            _toDoItemRepository = toDoItemRepository;
        }

        // Methods
        public async Task<bool> AddToDoItem(string userName, ToDoItemDto toDoItem)
        {
            ToDoItem itemEntity = _mapper.Map<ToDoItem>(toDoItem);
            return await _toDoItemRepository.AddToDoItem(userName, itemEntity);
        }

        public async Task<bool> AddToDoItemToAll(ToDoItemDto toDoItem)
        {
            ToDoItem itemEntity = _mapper.Map<ToDoItem>(toDoItem);
            return await _toDoItemRepository.AddToDoItemToAll(itemEntity);
        }

        public async Task<bool> CompleteToDoItem(Guid id)
        {
            return await _toDoItemRepository.CompleteToDoItem(id);
        }

        public async Task<bool> DeleteToDoItem(Guid id)
        {
            return await _toDoItemRepository.DeleteToDoItem(id);
        }

        public async Task<ToDoItemDto> GetToDoItem(Guid id)
        {
            ToDoItem item = await _toDoItemRepository.GetToDoItem(id);
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
