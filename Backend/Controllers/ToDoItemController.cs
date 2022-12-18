using Backend.DTOs;
using Backend.Entities.Exceptions;
using Backend.Interfaces;
using Backend.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>Controller for the To-Do API</summary>
    public class ToDoItemController : BaseApiController
    {
        private readonly IToDoItemService _toDoItemService;
        private readonly IUserService _userService;

        // Constructor

        /// <summary>Initializes a new instance of the <see cref="ToDoItemController"/> class.</summary>
        /// <param name="toDoItemService">The to-do item service.</param>
        /// <param name="userService">The user service.</param>
        public ToDoItemController(IToDoItemService toDoItemService, IUserService userService)
        {
            _toDoItemService = toDoItemService;
            _userService = userService;
        }

        // Endpoints

        /// <summary>Adds a new to-do item.</summary>
        /// <param name="userName">The username.</param>
        /// <param name="toDoItem">The to-do item.</param>
        /// <returns>The added to-do item.</returns>
        /// <exception cref="ToDoListException">Thrown when the to-do item could not be added.</exception>
        [HttpPost("{userName}")]
        public async Task<ActionResult<ToDoItemDto>> AddToDoItem(string userName, ToDoItemDto toDoItem)
        {
            try
            {
                if (await _toDoItemService.AddToDoItem(userName, toDoItem))
                {
                    return Ok(toDoItem);
                }
                return BadRequest("Failed to add to-do item");
            }
            catch (ToDoListException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>Updates a to-do item.</summary>
        /// <param name="toDoItem">The to-do item to update.</param>
        /// <returns>The updated to-do item.</returns>
        [HttpPut]
        public async Task<ActionResult<ToDoItemDto>> UpdateToDoItem(ToDoItemDto toDoItem)
        {
            if (await _toDoItemService.UpdateToDoItem(toDoItem))
            {
                return Ok(toDoItem);
            }
            return BadRequest("Failed to update to-do item");
        }

        /// <summary>Gets all the to-do items.</summary>
        /// <returns>All the to-do items.</returns>
        [HttpGet("getAll")]
        public async Task<IEnumerable<ToDoItemDto>> GetToDoItems()
        {
            return await _toDoItemService.GetToDoItems();
        }

        /// <summary>Deletes a to-do item.</summary>
        /// <param name="id">The ID of the to-do item to delete.</param>
        /// <returns>The deleted to-do item.</returns>
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
            return BadRequest("Failed to delete to-do item");
        }

        /// <summary>Marks a to-do item as complete.</summary>
        /// <param name="id">The ID of the to-do item.</param>
        /// <param name="isComplete">Whether the to-do item should be marked as complete.</param>
        /// <returns>The to-do item.</returns>
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
            return BadRequest("Failed to complete to-do item");
        }

        /// <summary>Changes the star status of a to-do item.</summary>
        /// <param name="id">The ID of the to-do item.</param>
        /// <param name="isStarred">The new star status.</param>
        /// <returns>The updated to-do item.</returns>
        [HttpPut("star/{id}")]
        public async Task<ActionResult<ToDoItemDto>> StarToDoItem(Guid id, [FromBody] bool isStarred)
        {
            var item = await _toDoItemService.GetToDoItem(id);

            if (item == null)
            {
                return NotFound(id);
            }

            if (await _toDoItemService.ChangeStarToDoItem(id, isStarred))
            {
                return Ok(item);
            }
            return BadRequest("Failed to star to-do item");
        }

        /// <summary>Adds a to-do item to all departments.</summary>
        /// <param name="toDoItem">The to-do item to add.</param>
        /// <param name="department">The department to add the to-do item to.</param>
        /// <returns>The added to-do item.</returns>
        /// <exception cref="ToDoListException">Thrown when the to-do item couldn't be added to all departments.</exception>
        [HttpPost("addToAllDepartment/{department}")]
        public async Task<ActionResult<ToDoItemDto>> AddToDoItemToAll(ToDoItemDto toDoItem, string department)
        {
            try
            {
                if (await _toDoItemService.AddToDoItemToAllByDepartment(toDoItem, EnumStringify.DepartmentEnumarator(department)))
                {
                    return Ok(toDoItem);
                }
                return BadRequest("Failed to add to-do item to all");
            }
            catch (ToDoListException e)
            {
                var result = Accepted();
                result.Value = e.Message;
                return Accepted(result);
            }
        }

        /// <summary>Gets a ToDoItem.</summary>
        /// <param name="id">The ID of the ToDoItem.</param>
        /// <returns>The ToDoItem.</returns>
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

        /// <summary>Gets the to-do list for an exchange coordinator.</summary>
        /// <param name="userName">The username of the exchange coordinator.</param>
        /// <returns>The to-do list for the exchange coordinator.</returns>
        [HttpGet("coordinatorToDoList/{userName}")]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetCoordinatorToDoList(string userName)
        {
            var coordinator = await _userService.GetExchangeCoordinator(userName);
            if (coordinator == null)
            {
                return NotFound(userName);
            }

            if (coordinator.ToDoList == null)
            {
                return BadRequest("Coordinator does not have a to-do list");
            }

            return Ok(coordinator.ToDoList);
        }

        /// <summary>Gets the to-do list for a student.</summary>
        /// <param name="userName">The username of the student.</param>
        /// <returns>The to-do list for the student.</returns>
        [HttpGet("studentToDoList/{userName}")]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetStudentToDoList(string userName)
        {
            var student = await _userService.GetStudent(userName);
            if (student == null)
            {
                return NotFound(userName);
            }

            if (student.ToDoList == null)
            {
                return BadRequest("Student does not have a to-do list");
            }

            return Ok(student.ToDoList);
        }
    }
}
