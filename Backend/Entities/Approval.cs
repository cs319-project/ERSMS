namespace Backend.Entities
{
    public class Approval
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfApproval { get; set; }
        public Boolean IsApproved { get; set; }
        // TODO think about the signature integration
    }
}
