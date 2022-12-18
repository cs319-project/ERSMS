using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    /// <summary>A class for representing a placement table (exchange score table).</summary>
    [Table("PlacementTables")]
    public class PlacementTable
    {
        [Key]
        public Guid Id { get; set; }

        public DepartmentInfo Department { get; set; }

        public string FileName { get; set; }

        public byte[] ExcelFile { get; set; }

        public DateTime UploadTime { get; set; }
    }
}
