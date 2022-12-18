using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
