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
    /// <summary>Controller for authentication.</summary>
    public class AuthenticationController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly DataContext _context;

        /// <summary>Initializes a new instance of the <see cref="AuthenticationController"/> class.</summary>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="context">The data context.</param>
        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService, DataContext context)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _context = context;
        }

        /// <summary>Registers a new user.</summary>
        /// <param name="register">The user's registration details.</param>
        /// <returns>The user's registration details.</returns>
        /// <exception cref="Exception">Thrown when the user is already registered.</exception>
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto register)
        {
            try
            {
                var result = await _authenticationService.Register(register);

                var user = _userService.GetUser(result.UserName).Result;
                result.UserDetails = user;

                return (result != null) ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>Logs in a user.</summary>
        /// <param name="login">The login details.</param>
        /// <returns>The user's details.</returns>
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

        /// <summary>Logs the user out.</summary>
        [HttpPost("logout")]
        public async Task<ActionResult> LogOut()
        {
            await _authenticationService.LogOut();
            return Ok();
        }
    }
}
