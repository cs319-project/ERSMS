using System.Runtime.Serialization;

namespace Backend.Utilities.Enum
{
    public enum Semester
    {
        [EnumMember(Value = "Fall")]
        Fall,

        [EnumMember(Value = "Spring")]
        Spring,

        [EnumMember(Value = "Full Year")]
        FullYear
    }
}
