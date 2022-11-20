using Backend.Entities.Enums;

namespace Backend.Entities
{
    // This can be divided into inheritance relations as exemptedelective, exemptedgeneric rather than using enum and nullables
    public class ExemptedCourse
    {
        public Guid Id { get; set; }
        public int Credits { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public CourseTypes CourseType { get; set; }
    }
}
