using Backend.Utilities.Enum;

namespace Backend.Entities
{

    /// <summary>A class that represents a semester.</summary>
    /// <remarks>
    ///     <para>
    ///         This class is used to represent a semester.
    ///     </para>
    ///     <para>
    ///         The <see cref="AcademicYear"/> property is used to represent the academic year of the semester.
    ///     </para>
    ///     <para>
    ///         The <see cref="Semester"/> property is used to represent the semester.
    ///     </para>
    /// </remarks>
    public class SemesterInfo
    {
        public string AcademicYear { get; set; } = "2022-2023";
        public Semester Semester { get; set; }
    }
}
