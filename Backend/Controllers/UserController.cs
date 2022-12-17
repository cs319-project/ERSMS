using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Backend.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult> GetUser(string username)
        {
            var user = await _userService.GetUser(username);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpGet("placedstudent/getall")]
        public async Task<ActionResult> GetStudents()
        {
            var students = await _userService.GetPlacedStudents();
            return students != null ? Ok(students) : NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return users != null ? Ok(users) : NotFound();
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateUser([FromBody] Object dto)
        {
            var json = (JObject)JsonConvert.DeserializeObject(dto.ToString());

            var updateUser = await _userService.UpdateUser(json);
            return updateUser != null ? Ok(updateUser) : NotFound();
        }

        [HttpDelete("delete/{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            var deleteUser = await _userService.DeleteUser(username);
            return deleteUser ? Ok(deleteUser) : NotFound();
        }
    }
}
