using System.Globalization;
using Backend.Entities;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Backend.Utilities
{
    /// <summary>
    /// <para>
    /// This class is a placeholder class for implementing the auto-placement algorithm.
    /// It initiates a school which is given in the placement table (exchange score table).
    /// </para>
    /// </summary>
    public class SchoolInfo
    {
        public string Name;
        public int Capacity;
        public int CurrentCount;
        public int RemainingCapacity => Capacity - CurrentCount;
        public SchoolInfo(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            CurrentCount = 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return this == null;
            }
            else
            {
                SchoolInfo? s = obj as SchoolInfo;
                return s.Name == Name;
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    /// <summary>
    /// <para>
    /// This class is a singleton class that is used to place students.
    /// It takes the placement table (exchange score table)'s Excel file as input, then parses the schools in that table.
    /// Lastly, it places the students based on their preferences and exchange scores.
    /// </para>
    /// </summary>
    public static class AutoPlacer
    {
        private static HashSet<SchoolInfo> Schools = new HashSet<SchoolInfo>();

        /// <summary>Parses the placement table's Excel file and returns a list of placed students.</summary>
        /// <param name="excelFile">The Excel file to parse.</param>
        /// <returns>A list of placed students.</returns>
        public static ICollection<PlacedStudent> PlaceStudents(byte[] excelFile)
        {
            Schools = new HashSet<SchoolInfo>();
            ICollection<PlacedStudent> placedStudents = new List<PlacedStudent>();

            if (Schools.Count == 0)
            {
                parseSchools(excelFile);
            }

            using (var fs = new MemoryStream(excelFile))
            {
                var workbook = new XSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(0);

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);

                    PlacedStudent student = new PlacedStudent();
                    student.PreferredSchools = new List<string>();

                    // iterate over the columns of the row
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell == null || cell.CellType == CellType.Blank)
                            continue;

                        else
                        {
                            // Get the top cell at the column
                            var topCell = sheet.GetRow(0).GetCell(j);
                            var topCellValue = topCell.StringCellValue;
                            var cellValue = cell.CellType == CellType.Numeric ? cell.NumericCellValue.ToString() : cell.StringCellValue;

                            if (topCellValue.StartsWith("First Name"))
                            {
                                TextInfo textInfo = new CultureInfo("tr-TR", false).TextInfo;
                                student.FirstName = textInfo.ToTitleCase(textInfo.ToLower(cellValue));
                            }
                            else if (topCellValue.StartsWith("Lastname"))
                            {
                                TextInfo textInfo = new CultureInfo("tr-TR", false).TextInfo;
                                student.LastName = textInfo.ToTitleCase(textInfo.ToLower(cellValue));
                            }
                            else if (topCellValue.StartsWith("Student ID Number"))
                            {
                                student.UserName = cellValue;
                            }
                            else if (topCellValue.StartsWith("Faculty"))
                            {
                                student.Department = new DepartmentInfo();
                                student.Department.FacultyName = 0;
                            }
                            else if (topCellValue.StartsWith("Department"))
                            {
                                student.Department.DepartmentName = Utilities.EnumStringify.DepartmentEnumarator(cellValue);
                            }
                            else if (topCellValue.StartsWith("Transcript Grade(4/4)"))
                            {
                                student.CGPA = Double.Parse(cellValue);
                            }
                            else if (topCellValue.StartsWith("Total Points"))
                            {
                                if (cell.CellType == CellType.Blank || String.IsNullOrEmpty(cellValue))
                                {
                                    break;
                                }
                                else
                                {
                                    student.ExchangeScore = Double.Parse(cellValue);
                                }
                            }
                            else if (topCellValue.StartsWith("Duration Preferred"))
                            {
                                student.PreferredSemester = new SemesterInfo();
                                student.PreferredSemester.Semester = Utilities.EnumStringify.SemesterEnumarator(cellValue);
                            }
                            else if (topCell.StringCellValue.StartsWith("Preferred"))
                            {

                                SchoolInfo school = null;
                                if (cell.CellType == CellType.Numeric)
                                {
                                    school = new SchoolInfo(cell.NumericCellValue.ToString(), 2);
                                }
                                else if (cell.CellType == CellType.String)
                                {
                                    school = new SchoolInfo(cell.StringCellValue, 2);
                                }
                                else
                                {
                                    continue;
                                }

                                SchoolInfo schoolInList = null;

                                if (Schools.Contains(school))
                                {
                                    schoolInList = Schools.First(s => s.Equals(school));

                                }

                                if (schoolInList != null)
                                {
                                    student.PreferredSchools.Add(schoolInList.Name);
                                }

                                if (schoolInList != null && schoolInList.RemainingCapacity > 0 && student.IsPlaced == false)
                                {
                                    schoolInList.CurrentCount++;

                                    student.ExchangeSchool = schoolInList.Name;
                                    student.IsPlaced = true;
                                }
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(student.FirstName))
                    {
                        placedStudents.Add(student);
                    }
                }
            }

            return placedStudents;
        }

        /// <summary>Parses the schools from the placement table's excel file.</summary>
        /// <param name="excelFile">The Excel file.</param>
        public static void parseSchools(byte[] excelFile)
        {
            using (var fs = new MemoryStream(excelFile))
            {
                var workbook = new XSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(0);
                var columns = sheet.GetRow(0).Cells.Select(c => c.StringCellValue).ToList();

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);

                    // iterate over the columns of the row
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell == null || cell.CellType == CellType.Blank)
                            continue;

                        else
                        {
                            // Get the top cell at the column
                            var topCell = sheet.GetRow(0).GetCell(j);

                            if (topCell.StringCellValue.StartsWith("Preferred"))
                            {
                                if (cell.CellType == CellType.Numeric)
                                {
                                    SchoolInfo school = new SchoolInfo(cell.NumericCellValue.ToString(), 2);
                                    Schools.Add(school);
                                }
                                else if (cell.CellType == CellType.String)
                                {
                                    SchoolInfo school = new SchoolInfo(cell.StringCellValue, 2);
                                    Schools.Add(school);

                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
