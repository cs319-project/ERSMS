using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

    }
}
