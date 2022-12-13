using Backend.Utilities.Enum;

namespace Backend.Entities
{
    public class SemesterInfo
    {
        public string AcademicYear { get; set; } = "2022-2023";
        public Semester Semester { get; set; }
    }
}
