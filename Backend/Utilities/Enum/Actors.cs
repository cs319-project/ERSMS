using System.Runtime.Serialization;

namespace Backend.Utilities.Enum
{
    public enum Actors
    {

        [EnumMember(Value = "Student")]
        Student,
        [EnumMember(Value = "Course Coordinator/Instructor")]
        CourseCoordinatorInstructor,
        [EnumMember(Value = "Exchange Coordinator")]
        ExchangeCoordinator,
        [EnumMember(Value = "Dean/Department Chair")]
        DeanDepartmentChair,
        [EnumMember(Value = "Admin")]
        Admin,
        [EnumMember(Value = "Office of International Students and Exchange Programs")]
        OISEP
    }
}
