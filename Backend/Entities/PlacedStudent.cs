using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    public class PlacedStudent
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentId { get; set; }
        public DepartmentInfo Department { get; set; }
        public double CGPA { get; set; }
        public double ExchangeScore { get; set; }
        public SemesterInfo PreferredSemester { get; set; }
        public ICollection<string> PreferredSchools { get; set; }
        public string ExchangeSchool { get; set; }
    }
}
