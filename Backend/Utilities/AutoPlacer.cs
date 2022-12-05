using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using System;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace Backend.Utilities
{
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

    public static class AutoPlacer
    {
        private static HashSet<SchoolInfo> Schools = new HashSet<SchoolInfo>();
        public static ICollection<PlacedStudent> autoPlacer()
        {
            return placeStudents();
        }

        private static ICollection<PlacedStudent> placeStudents()
        {
            ICollection<PlacedStudent> placedStudents = new List<PlacedStudent>();

            if (Schools.Count == 0)
            {
                parseSchools();
            }

            using (var fs = new FileStream("/Users/friberk/Downloads/Selection 2022-2023.xlsx", FileMode.Open, FileAccess.Read))
            {
                var workbook = new XSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(0);

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);

                    PlacedStudent student = new PlacedStudent();

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
                                student.FirstName = cellValue;
                            }
                            else if (topCellValue.StartsWith("Lastname"))
                            {
                                var temp = cellValue.Substring(1, cellValue.Length - 1).ToLower();
                                student.LastName = cellValue[0] + temp;
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
        public static void parseSchools()
        {
            using (var fs = new FileStream("/Users/friberk/Downloads/Selection 2022-2023.xlsx", FileMode.Open, FileAccess.Read))
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
