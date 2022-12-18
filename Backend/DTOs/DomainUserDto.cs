using System.Text.Json.Serialization;

namespace Backend.DTOs
{
    /// <summary>A data transfer object for a domain user.</summary>
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
