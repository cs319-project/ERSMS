using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    public class Approval
    {
        //[ForeignKey("CTEForm")]
        //public Guid CTEFormId { get; set; }
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime DateOfApproval { get; set; }
        public string Comment { get; set; }
        public Boolean IsApproved { get; set; } = false;
        // TODO think about the signature integration
    }
}
