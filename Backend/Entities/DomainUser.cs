using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

/*
    Actor based inheritance is problematic in .NET. We either choose to have a single class
    which include all fields with a list of permissions (ViewForms, EditForms, etc.), or we
    can have a class for each actor type as desired but indeed using a workaround.

    Still, probably we are going to include a kind of permission system, but due to scope of
    the project and course, it is better to have different actors in seperated classes.

    See:
    https://stackoverflow.com/a/40682239

*/

namespace Backend.Entities
{
    public abstract class DomainUser // : IAggregateRoot
    {
        [Required] public Guid Id { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public Guid AppUserId { get; private set; }
        public AppUser IdentityUser { get; set; } = null!;
        public string Discriminator { get; private set; } = null!;
    }
}
