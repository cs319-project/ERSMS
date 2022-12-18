namespace Backend.DTOs
{
    /// <summary>A data transfer object for an approval.</summary>
    public class ApprovalDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfApproval { get; set; } = DateTime.Now;
        public Boolean IsApproved { get; set; }
        public string Comment { get; set; } = "";
    }
}
