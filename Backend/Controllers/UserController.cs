using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Backend.Controllers
{
    /// <summary>Controller for the User API.</summary>
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        /// <summary>Creates a new instance of the <see cref="UserController"/> class.</summary>
        /// <param name="userService">The user service.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>Gets a user by their username.</summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user with the specified username.</returns>
        [HttpGet("{username}")]
        public async Task<ActionResult> GetUser(string username)
        {
            var user = await _userService.GetUser(username);
            return user != null ? Ok(user) : NotFound();
        }

        /// <summary>Gets all placed students.</summary>
        /// <returns>A list of placed students.</returns>
        [HttpGet("placedstudent/getall")]
        public async Task<ActionResult> GetStudents()
        {
            var students = await _userService.GetPlacedStudents();
            return students != null ? Ok(students) : NotFound();
        }

        /// <summary>Gets all users.</summary>
        /// <returns>All users.</returns>
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return users != null ? Ok(users) : NotFound();
        }

        /// <summary>Updates a user.</summary>
        /// <param name="dto">The user to update.</param>
        /// <returns>The updated user.</returns>
        [HttpPut("update")]
        public async Task<ActionResult> UpdateUser([FromBody] Object dto)
        {
            var json = (JObject)JsonConvert.DeserializeObject(dto.ToString());

            var updateUser = await _userService.UpdateUser(json);
            return updateUser != null ? Ok(updateUser) : NotFound();
        }

        /// <summary>Deletes a user.</summary>
        /// <param name="username">The username of the user to delete.</param>
        /// <returns>An <see cref="ActionResult"/> indicating whether the user was deleted.</returns>
        [HttpDelete("delete/{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            var deleteUser = await _userService.DeleteUser(username);
            return deleteUser ? Ok(deleteUser) : NotFound();
        }

        /// <summary>Gets the students from the same school.</summary>
        /// <param name="username">The username.</param>
        /// <returns>The students from the same school.</returns>
        [HttpGet("student/{username}/sameSchool")]
        public async Task<ActionResult> GetStudentsFromSameSchool(string username)
        {
            var students = await _userService.GetStudentsFromSameSchool(username);
            return students != null ? Ok(students) : NotFound();
        }

        /// <summary>Gets the registered student's mail id tuple.</summary>
        /// <returns>The mail id tuple of registered students.</returns>
        [HttpGet("registeredStudents/tuples")]
        public async Task<ActionResult> GetRegisteredStudentsTuples()
        {
            var studentsTuples = await _userService.GetRegisteredStudentsTuples();
            return (studentsTuples != null && studentsTuples.Count() > 0) ? Ok(studentsTuples) : NotFound();
        }
    }
}
