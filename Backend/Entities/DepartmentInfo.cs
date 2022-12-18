using System.ComponentModel.DataAnnotations.Schema;
using Backend.Utilities.Enum;

namespace Backend.Entities
{
    public class DepartmentInfo
    {
        //public Guid Id { get; set; } // NEWLY ADDED
        [Column("DepartmentName")]
        public Department DepartmentName { get; set; }
        [Column("FacultyName")]
        public Faculty FacultyName { get; set; }

        public override bool Equals(object obj)
        {
            var info = obj as DepartmentInfo;
            return info.DepartmentName == DepartmentName && info.FacultyName == FacultyName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DepartmentName, FacultyName);
        }
    }
}
