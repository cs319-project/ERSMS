namespace Backend.DTOs
{
    /// <summary>A data transfer object for a placement table (exchange score table).</summary>
    public class PlacementTableDto
    {
        public Guid Id { get; set; }

        public DepartmentInfoDto Department { get; set; }

        public string FileName { get; set; }

        // public byte[] ExcelFile { get; set; }

        public DateTime UploadTime { get; set; }
    }
}
