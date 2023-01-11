using System;
using System.Drawing;
using System.IO;
using Backend.DTOs;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Rectangle = iTextSharp.text.Rectangle;

namespace Backend.Utilities
{
    public static class PDFGenerator
    {
        public static void GeneratePreAppPdf(PreApprovalFormDto form)
        {
            string originalFile = "preapp.pdf";

            PdfReader reader = new PdfReader(originalFile);
            using (FileStream fs = new FileStream("bin/filled.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            // Creating iTextSharp.text.pdf.PdfStamper object to write
            // Data from iTextSharp.text.pdf.PdfReader object to FileStream object
            using (PdfStamper stamper = new PdfStamper(reader, fs))
            {
                PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);
                // Getting the Page Size
                Rectangle rect = reader.GetPageSize(1);

                // Get the ContentByte object
                PdfContentByte cb = stamper.GetUnderContent(1);

                // Tell the cb that the next commands should be "bound" to this new layer
                cb.SetFontAndSize(BaseFont.CreateFont(
                    BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10);

                PdfGState gState = new PdfGState();
                cb.SetGState(gState);

                cb.SetColorFill(BaseColor.BLACK);
                cb.BeginText();

                // Student name
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.FirstName, 78, 503, 0);
                // Student surname
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.LastName, 78, 483, 0);
                // Student id
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.IDNumber, 487, 503, 0);
                // Department
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.Department, 487, 483, 0);
                // Academic Year
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.AcademicYear, 487, 455, 0);
                // Semester
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.Semester, 487, 430, 0);
                // School
                var school = form.HostUniversityName;
                if (school.Length >= 46)
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, school.Substring(0, 46), 168, 455, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, school.Substring(46), 168, 435, 0);
                }
                else
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, school, 168, 455, 0);
                }
                var courses = form.RequestedCourseGroups.ToList();
                var height = 338;
                var lastNotMerged = 0;
                var curRow = 1;
                for (int i = 0; i < courses.Count(); i++)
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, courses[i].RequestedExemptedCourse.CourseType == "Mandatory Course" ? courses[i].RequestedExemptedCourse.CourseCode + courses[i].RequestedExemptedCourse.CourseName : courses[i].RequestedExemptedCourse.CourseType, 392, height, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, courses[i].RequestedExemptedCourse.BilkentCredits.ToString(), 635, height, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, courses[i].RequestedExemptedCourse.CourseType == "Mandatory Course" ? "" : courses[i].RequestedExemptedCourse.CourseCode == null ? "" : courses[i].RequestedExemptedCourse.CourseCode, 670, height, 0);
                    lastNotMerged = curRow;
                    for (int j = 0; j < courses[i].RequestedCourses.Count(); j++)
                    {
                        var requests = courses[i].RequestedCourses.ToList();
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, requests[j].CourseCode, 45, height, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, requests[j].CourseName, 100, height, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, requests[j].ECTS.ToString(), 355, height, 0);
                        if (j >= 1)
                        {
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Merged with row " + lastNotMerged + "*", 392, height, 0);
                        }
                        curRow++;
                        height -= 23;
                    }
                }
                // Coord. Name
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.ExchangeCoordinatorApproval != null ? form.ExchangeCoordinatorApproval.Name : "", 185, 87, 0);
                // Date
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.ExchangeCoordinatorApproval != null ? form.ExchangeCoordinatorApproval.DateOfApproval.ToLongDateString() : "", 670, 87, 0);


                cb.EndText();
            }
        }

        public static void GenerateCTEPdf(CTEFormDto form)
        {
            string originalFile = "cte.pdf";

            PdfReader reader = new PdfReader(originalFile);
            using (FileStream fs = new FileStream("bin/filledCTE.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            // Creating iTextSharp.text.pdf.PdfStamper object to write
            // Data from iTextSharp.text.pdf.PdfReader object to FileStream object
            using (PdfStamper stamper = new PdfStamper(reader, fs))
            {
                PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);
                // Getting the Page Size
                Rectangle rect = reader.GetPageSize(1);

                // Get the ContentByte object
                PdfContentByte cb = stamper.GetUnderContent(1);

                // Tell the cb that the next commands should be "bound" to this new layer
                cb.SetFontAndSize(BaseFont.CreateFont(
                    BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10);

                PdfGState gState = new PdfGState();
                cb.SetGState(gState);

                cb.SetColorFill(BaseColor.BLACK);
                cb.BeginText();

                // Student name
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.FirstName, 86, 521, 0);
                // Student surname
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.LastName, 86, 497, 0);
                // Student id
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.IDNumber, 520, 520, 0);
                // Department
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.Department, 520, 497, 0);
                // School
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, form.HostUniversityName, 194, 459, 0);

                var courses = form.TransferredCourseGroups.ToList();
                var height = 390;
                var lastNotMerged = 0;
                var curRow = 1;
                for (int i = 0; i < courses.Count(); i++)
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, courses[i].ExemptedCourse.CourseType == "Mandatory Course" ? courses[i].ExemptedCourse.CourseCode + courses[i].ExemptedCourse.CourseName : courses[i].ExemptedCourse.CourseType, 372, height, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, courses[i].ExemptedCourse.BilkentCredits.ToString(), 601, height, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, courses[i].ExemptedCourse.CourseType == null && courses[i].ExemptedCourse.CourseType == "Mandatory Course" ? "" : courses[i].ExemptedCourse.CourseCode != null ? courses[i].ExemptedCourse.CourseCode : "", 630, height, 0);
                    lastNotMerged = curRow;
                    for (int j = 0; j < courses[i].TransferredCourses.Count(); j++)
                    {
                        var requests = courses[i].TransferredCourses.ToList();
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, requests[j].CourseCode, 60, height, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, requests[j].CourseName, 122, height, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, requests[j].ECTS.ToString(), 289, height, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, requests[j].Grade, 330, height, 0);
                        if (j >= 1)
                        {
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Merged with row " + lastNotMerged + "*", 372, height, 0);
                        }
                        curRow++;
                        height -= curRow > 5 ? 26 : 27;
                    }
                }
                // Coord. Name
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, (form.ExchangeCoordinatorApproval != null && form.ExchangeCoordinatorApproval.IsApproved) ? form.ExchangeCoordinatorApproval.Name : "", 327, 107, 0);
                // Date
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, (form.ExchangeCoordinatorApproval != null && form.ExchangeCoordinatorApproval.IsApproved) ? form.ExchangeCoordinatorApproval.DateOfApproval.ToShortDateString() : "", 699, 107, 0);
                // Chair Name
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, (form.ChairApproval != null && form.ChairApproval.IsApproved) ? form.ChairApproval.Name : "", 327, 92, 0);
                // Date
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, (form.ChairApproval != null && form.ChairApproval.IsApproved) ? form.ChairApproval.DateOfApproval.ToShortDateString() : "", 699, 92, 0);
                // Dean Name
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, (form.DeanApproval != null && form.DeanApproval.IsApproved) ? form.DeanApproval.Name : "", 327, 77, 0);
                // Date
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, (form.DeanApproval != null && form.DeanApproval.IsApproved) ? form.DeanApproval.DateOfApproval.ToShortDateString() : "", 699, 77, 0);

                cb.EndText();
            }
        }
    }
}
