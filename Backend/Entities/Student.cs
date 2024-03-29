using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    /// <summary>A class for representing a student.</summary>
    [Table("Students")]
    public class Student : DomainUser
    {
        public int EntranceYear { get; set; } // might be redundant

        public DepartmentInfo Major { get; set; }

        public ICollection<DepartmentInfo> Minors { get; set; }

        public double CGPA { get; set; }

        public double ExchangeScore { get; set; }

        public SemesterInfo PreferredSemester { get; set; }

        public ICollection<string> PreferredSchools { get; set; }

        public string ExchangeSchool { get; set; }

        public ICollection<CTEForm> CTEForms { get; set; }

        public ICollection<PreApprovalForm> PreApprovalForms { get; set; }

        public ICollection<EquivalenceRequest> EquivalenceRequestForms { get; set; }

        public ICollection<ToDoItem> ToDoList { get; set; } = new List<ToDoItem>();
    }
}
