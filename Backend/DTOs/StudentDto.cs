namespace Backend.DTOs
{
    /// <summary>A data transfer object for a student.</summary>
    public class StudentDto : DomainUserDto
    {
        public int EntranceYear { get; set; } // might be redundant

        public DepartmentInfoDto Major { get; set; }

        public ICollection<DepartmentInfoDto> Minors { get; set; }

        public double CGPA { get; set; }

        public double ExchangeScore { get; set; }

        public SemesterInfoDto PreferredSemester { get; set; }

        public ICollection<string> PreferredSchools { get; set; }

        public string ExchangeSchool { get; set; }

        public ICollection<CTEFormDto> CTEForms { get; set; }

        public ICollection<PreApprovalFormDto> PreApprovalForms { get; set; }

        public ICollection<EquivalenceRequestDto> EquivalenceRequestForms { get; set; }

        public ICollection<ToDoItemDto> ToDoList { get; set; }
    }
}
