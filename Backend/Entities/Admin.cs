using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    /// <summary>A domain user with administrative privileges.</summary>
    [Table("Admins")]
    public class Admin : DomainUser
    {

    }
}
