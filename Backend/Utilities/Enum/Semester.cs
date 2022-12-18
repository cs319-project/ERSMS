using System.Runtime.Serialization;

namespace Backend.Utilities.Enum
{
    /// <summary>An enum for representing the semesters.</summary>
    public enum Semester
    {
        [EnumMember(Value = "Fall Semester")]
        Fall,

        [EnumMember(Value = "Spring Semester")]
        Spring,

        [EnumMember(Value = "Full Year")]
        FullYear
    }
}
