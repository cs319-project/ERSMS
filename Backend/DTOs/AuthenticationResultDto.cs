namespace Backend.DTOs
{
    /// <summary>A data transfer object for authentication results.</summary>
    public class AuthenticationResultDto
    {
        public string UserName { get; set; }
        public Object UserDetails { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
