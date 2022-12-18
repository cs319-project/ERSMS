using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    /// <summary>A class for representing a placed student.</summary>
    [Table("PlacedStudents")]
    public class PlacedStudent
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DepartmentInfo Department { get; set; }
        public double CGPA { get; set; }
        public double ExchangeScore { get; set; }
        public SemesterInfo PreferredSemester { get; set; }
        public ICollection<string> PreferredSchools { get; set; }
        public string? ExchangeSchool { get; set; } = null;
        public bool IsPlaced { get; set; } = false;
    }
}
