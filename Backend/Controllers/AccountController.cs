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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenservice)
        {
            _tokenService = tokenservice;
            _context = context;
        }

        // UserExists
        private Task<bool> UserExists(string mail)
        {
            return _context.ErsmsUsers.AnyAsync(x => x.Mail == mail);
        }

        // CreatePasswordHash
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // VerifyPasswordHash
        private bool VerifyPasswordHash(in string password, in byte[] passwordHash, in byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);

            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            // Check if the password is correct
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ErsmsUserDto>> Register([FromBody] RegisterDto registerDto)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            // Trim fields
            string mail = registerDto.Mail.Trim().ToLower();
            string firstName = textInfo.ToTitleCase(registerDto.FirstName.Trim().ToLower());
            string lastName = textInfo.ToTitleCase(registerDto.LastName.Trim().ToLower());
            string password = registerDto.Password;

            // Check if the user already exists
            if (await UserExists(mail))
            {
                return BadRequest("User already exists.");
            }

            // Create a new user
            var user = new ErsmsUser
            {
                Mail = mail,
                FirstName = firstName,
                LastName = lastName
            };

            // Create a password hash and salt
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            // Set the password hash and salt
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Add the user to the database
            _context.ErsmsUsers.Add(user);
            await _context.SaveChangesAsync();

            // Return the user
            return new ErsmsUserDto
            {
                Mail = user.Mail,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<ErsmsUserDto>> Login([FromBody] LoginDto loginDto)
        {
            // Trim fields
            string mail = loginDto.Mail.Trim().ToLower();
            string password = loginDto.Password;

            var user = await _context.ErsmsUsers.SingleOrDefaultAsync(x => x.Mail == mail);

            // Check if the user exists
            if (user == null)
            {
                return Unauthorized("Invalid mail.");
            }

            // Check if the password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid password.");
            }

            // Return the user
            return new ErsmsUserDto
            {
                Mail = user.Mail,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
