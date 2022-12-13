using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class PlacedStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DepartmentInfoDto Department { get; set; }
        public double CGPA { get; set; }
        public double ExchangeScore { get; set; }
        public SemesterInfoDto PreferredSemester { get; set; }
        public ICollection<string> PreferredSchools { get; set; }
        public string? ExchangeSchool { get; set; } = null;
        public bool IsPlaced { get; set; } = false;
    }
}
