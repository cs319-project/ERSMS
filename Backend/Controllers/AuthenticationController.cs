using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Backend.Data;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly DataContext _context;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService, DataContext context)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto register)
        {
            try
            {
                var result = await _authenticationService.Register(register);
                return (result != null) ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LogIn(LoginDto login)
        {
            try
            {
                var result = await _authenticationService.LogIn(login);

                var user = _userService.GetUser(result.UserName).Result;
                result.UserDetails = user;

                return (result != null) ? Ok(result) : Unauthorized(result);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<ActionResult> LogOut()
        {
            await _authenticationService.LogOut();
            return Ok();
        }
    }
}
