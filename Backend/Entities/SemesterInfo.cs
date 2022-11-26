using Backend.Utilities.Enum;

namespace Backend.Entities
{
    public class SemesterInfo
    {
        public string AcademicYear { get; set; } = null;
        public Semester Semester { get; set; }
    }
}
