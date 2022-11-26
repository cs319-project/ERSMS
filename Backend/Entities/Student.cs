using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    [Table("Students")]
    public class Student : DomainUser
    {
        public int EntranceYear { get; set; } // might be redundant

        public ICollection<DepartmentInfo> Majors { get; set; }

        public ICollection<DepartmentInfo> Minors { get; set; }

        public double CGPA { get; set; }

        public double ExchangeScore { get; set; }

        public SemesterInfo PreferredSemester { get; set; }

        public ICollection<string> PreferredSchools { get; set; }

        public string ExchangeSchool { get; set; }

        public ICollection<CTEForm> CTEForms { get; set; }

        public ICollection<PreApprovalForm> PreApprovalForms { get; set; }
    }
}
