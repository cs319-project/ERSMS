using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.DataAnnotations;

namespace Backend.DTOs
{
    public class RegisterDto
    {
        [BilkentMailAttribute] public string Mail { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Password { get; set; }
    }
}
