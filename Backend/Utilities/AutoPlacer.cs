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
        public static HashSet<SchoolInfo> Schools = new HashSet<SchoolInfo>();
        public static void autoPlacer()
        {


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
