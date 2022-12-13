using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class PlacementTableDto
    {
        public Guid Id { get; set; }

        public DepartmentInfoDto Department { get; set; }

        public string FileName { get; set; }

        // public byte[] ExcelFile { get; set; }

        public DateTime UploadTime { get; set; }
    }
}
