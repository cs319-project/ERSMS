using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class StudentDto : DomainUserDto
    {
        public int EntranceYear { get; set; } // might be redundant

        public ICollection<DepartmentInfoDto> Majors { get; set; }

        public ICollection<DepartmentInfoDto> Minors { get; set; }

        public double CGPA { get; set; }

        public double ExchangeScore { get; set; }

        public SemesterInfoDto PreferredSemester { get; set; }

        public ICollection<string> PreferredSchools { get; set; }

        public string ExchangeSchool { get; set; }

        public ICollection<CTEFormDto> CTEForms { get; set; }

        public ICollection<PreApprovalFormDto> PreApprovalForms { get; set; }

        public ICollection<ExemptionRequestForm> ExemptionRequestForms { get; set; }

    }
}
