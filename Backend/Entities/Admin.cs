using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    /// <summary>A domain user with administrative privileges.</summary>
    [Table("Admins")]
    public class Admin : DomainUser
    {

    }
}
