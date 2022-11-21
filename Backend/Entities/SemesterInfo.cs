using Backend.Utilities.Enum;

namespace Backend.Entities
{
    public struct SemesterInfo
    {
        public SemesterInfo(string wholeText)
        {
            if (String.IsNullOrEmpty(wholeText))
            {
                Semester = Semester.Fall;
                AcademicYear = new Tuple<int, int>(2021, 2022);
            }
            else
            {
                var parts = wholeText.Trim().Split(' ');
                string year = parts[0];
                Semester = (Semester)Enum.Parse(typeof(Semester), parts[1]);

                var yearParts = year.Split('-');
                this.AcademicYear = new Tuple<int, int>(int.Parse(yearParts[0]), int.Parse(yearParts[1]));
                AcademicYear = new Tuple<int, int>(2019, 2020);
            }
        }

        public Tuple<int, int> AcademicYear { get; set; } = null;
        public Semester Semester { get; set; }

        public override string ToString()
        {
            if (AcademicYear == null || Semester == null)
                return string.Empty;
            return $"{AcademicYear.Item1}-{AcademicYear.Item2} {Semester}";
        }
    }
}
