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
        private readonly DataContext _context;

        public AuthenticationController(IAuthenticationService authenticationService, DataContext context)
        {
            _authenticationService = authenticationService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto register)
        {
            var result = await _authenticationService.Register(register);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LogIn(LoginDto login)
        {
            var result = await _authenticationService.LogIn(login);
            return result.Succeeded ? Ok(result) : Unauthorized(result);
        }
    }
}
