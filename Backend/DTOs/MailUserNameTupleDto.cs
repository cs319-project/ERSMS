using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    ///<summary>Represents a tuple of a user's email and username.</summary>
    public class MailUserNameTupleDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
