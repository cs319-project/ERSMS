using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class ToDoItemController : BaseApiController
    {
        private readonly IToDoItemService _toDoItemService;
        private readonly IUserService _userService;

        // Constructor
        public ToDoItemController(IToDoItemService toDoItemService, IUserService userService)
        {
            _toDoItemService = toDoItemService;
            _userService = userService;
        }

        // Endpoints
        [HttpPost("{userName}")]
        public async Task<ActionResult<ToDoItemDto>> AddToDoItem(string userName, ToDoItemDto toDoItem)
        {
            if (await _toDoItemService.AddToDoItem(userName, toDoItem))
            {
                return Ok(toDoItem);
            }
            return BadRequest("Failed to add to do item");
        }

        [HttpPut()]
        public async Task<ActionResult<ToDoItemDto>> UpdateToDoItem(ToDoItemDto toDoItem)
        {
            if (await _toDoItemService.UpdateToDoItem(toDoItem))
            {
                return Ok(toDoItem);
            }
            return BadRequest("Failed to update to do item");
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<ToDoItemDto>> GetToDoItems()
        {
            return await _toDoItemService.GetToDoItems();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoItemDto>> DeleteToDoItem(Guid id)
        {
            var item = await _toDoItemService.GetToDoItem(id);

            if (item == null)
            {
                return NotFound(id);
            }

            if (await _toDoItemService.DeleteToDoItem(id))
            {
                return Ok(item);
            }
            return BadRequest("Failed to delete to do item");
        }

        [HttpPut("complete/{id}")]
        public async Task<ActionResult<ToDoItemDto>> CompleteToDoItem(Guid id, [FromBody] bool isComplete)
        {
            var item = await _toDoItemService.GetToDoItem(id);

            if (item == null)
            {
                return NotFound(id);
            }

            if (await _toDoItemService.ChangeCompleteToDoItem(id, isComplete))
            {
                return Ok(item);
            }
            return BadRequest("Failed to complete to do item");
        }

        [HttpPost("addToAll")]
        public async Task<ActionResult<ToDoItemDto>> AddToDoItemToAll(ToDoItemDto toDoItem)
        {
            if (await _toDoItemService.AddToDoItemToAll(toDoItem))
            {
                return Ok(toDoItem);
            }
            return BadRequest("Failed to add to do item to all");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemDto>> GetToDoItem(Guid id)
        {
            var item = await _toDoItemService.GetToDoItem(id);

            if (item == null)
            {
                return NotFound(id);
            }

            return Ok(item);
        }

        [HttpGet("coordinatorToDoList/{userName}")]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetCoordinatorToDoList(string userName)
        {
            var coordinator = await _userService.GetCoordinator(userName);
            if (coordinator == null)
            {
                return NotFound(userName);
            }

            if (coordinator.ToDoList == null)
            {
                return BadRequest("Coordinator does not have a to do list");
            }

            return Ok(coordinator.ToDoList);
        }
    }
}
