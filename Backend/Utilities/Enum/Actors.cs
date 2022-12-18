using System.Runtime.Serialization;

namespace Backend.Utilities.Enum
{
    /// <summary>An enum for representing the actors in the application.</summary>
    public enum Actors
    {

        [EnumMember(Value = "Student")]
        Student,
        [EnumMember(Value = "Course Coordinator Instructor")]
        CourseCoordinatorInstructor,
        [EnumMember(Value = "Exchange Coordinator")]
        ExchangeCoordinator,
        [EnumMember(Value = "Dean Department Chair")]
        DeanDepartmentChair,
        [EnumMember(Value = "Admin")]
        Admin,
        [EnumMember(Value = "Office of International Students and Exchange Programs")]
        OISEP
    }
}
