using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    /// <summary>A class representing a dean or a department chair.</summary>
    /// <remarks>Deans and department chairs are distinguished by the IsDean property.</remarks>
    [Table("DeanDepartmentChairs")]
    public class DeanDepartmentChair : DomainUser
    {
        public DepartmentInfo Department { get; set; }
        public bool IsDean { get; set; } = false;
    }
}
