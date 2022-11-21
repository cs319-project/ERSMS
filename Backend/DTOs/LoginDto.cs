using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.DataAnnotations;

namespace Backend.DTOs
{
    public class LoginDto
    {
        [BilkentMailAttribute] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}
