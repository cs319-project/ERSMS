using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class DomainUserDto
    {
        public Guid Id { get; set; }
        public string ActorType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AppUserDto IdentityUser { get; set; }
        [JsonIgnore] public List<MessageDto> MessagesSent { get; set; }
        [JsonIgnore] public List<MessageDto> MessagesReceived { get; set; }
    }
}
