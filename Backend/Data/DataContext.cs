using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Backend.Data
{
    /// <summary>The data context for the application.</summary>
    public class DataContext : IdentityDbContext<AppUser, Role, Guid>
    {

        /// <summary>Initializes a new instance of the <see cref="DataContext"/> class.</summary>
        /// <param name="options">The options for this context.</param>
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>Configures the model builder.</summary>
        /// <param name="builder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DomainUser>().Navigation(u => u.IdentityUser).AutoInclude();
            builder.Entity<DomainUser>().Navigation(m => m.MessagesSent).AutoInclude();
            builder.Entity<DomainUser>().Navigation(m => m.MessagesReceived).AutoInclude();
            builder.Entity<AppUser>().Navigation(u => u.DomainUser).AutoInclude();

            builder.Entity<DomainUser>().HasMany<Message>(m => m.MessagesSent);
            builder.Entity<DomainUser>().HasMany<Message>(m => m.MessagesReceived);

            builder.Entity<ExchangeCoordinator>().Navigation(c => c.ToDoList).AutoInclude();
            builder.Entity<ExchangeCoordinator>().HasMany<ToDoItem>(c => c.ToDoList);

            builder.Entity<Student>().Navigation(c => c.ToDoList).AutoInclude();
            builder.Entity<Student>().HasMany<ToDoItem>(c => c.ToDoList);

            builder.Entity<LoggedEquivalentCourse>().Navigation(c => c.ExemptedCourse).AutoInclude();
            builder.Entity<LoggedEquivalentCourse>().HasOne<ExemptedCourse>(c => c.ExemptedCourse);

            builder.Entity<LoggedTransferredCourse>().Navigation(c => c.TransferredCourseGroups).AutoInclude();
            builder.Entity<LoggedTransferredCourse>().HasMany<TransferredCourseGroup>(c => c.TransferredCourseGroups);

            builder.Entity<CTEForm>().Navigation(c => c.TransferredCourseGroups).AutoInclude();
            builder.Entity<CTEForm>().Navigation(c => c.DeanApproval).AutoInclude();
            builder.Entity<CTEForm>().Navigation(c => c.ChairApproval).AutoInclude();
            builder.Entity<CTEForm>().Navigation(c => c.ExchangeCoordinatorApproval).AutoInclude();
            builder.Entity<CTEForm>().Navigation(c => c.FacultyOfAdministrationBoardApproval).AutoInclude();
            builder.Entity<TransferredCourseGroup>().Navigation(c => c.TransferredCourses).AutoInclude();
            builder.Entity<TransferredCourseGroup>().Navigation(c => c.ExemptedCourse).AutoInclude();
            builder.Entity<PreApprovalForm>().Navigation(c => c.RequestedCourseGroups).AutoInclude();
            builder.Entity<PreApprovalForm>().Navigation(c => c.ExchangeCoordinatorApproval).AutoInclude();
            builder.Entity<PreApprovalForm>().Navigation(c => c.FacultyAdministrationBoardApproval).AutoInclude();
            builder.Entity<RequestedCourseGroup>().Navigation(c => c.RequestedCourses).AutoInclude();
            builder.Entity<RequestedCourseGroup>().Navigation(c => c.RequestedExemptedCourse).AutoInclude();

            builder.Entity<Student>().Navigation(s => s.Major).AutoInclude();
            builder.Entity<Student>().Navigation(s => s.Minors).AutoInclude();
            builder.Entity<Student>().Navigation(s => s.CTEForms).AutoInclude();
            builder.Entity<Student>().Navigation(s => s.PreApprovalForms).AutoInclude();
            builder.Entity<Student>().Navigation(s => s.EquivalenceRequestForms).AutoInclude();

            builder.Entity<DeanDepartmentChair>().OwnsOne<DepartmentInfo>(c => c.Department);
            builder.Entity<ExchangeCoordinator>().OwnsOne<DepartmentInfo>(c => c.Department);

            builder.Entity<CourseCoordinatorInstructor>().OwnsOne(c => c.Course);
            builder.Entity<CourseCoordinatorInstructor>().OwnsOne<DepartmentInfo>(c => c.Department);

            builder.Entity<Student>().OwnsOne<DepartmentInfo>(c => c.Major);
            builder.Entity<Student>().OwnsMany<DepartmentInfo>(c => c.Minors);

            builder.Entity<Student>().OwnsOne<SemesterInfo>(c => c.PreferredSemester);

            builder.Entity<PlacedStudent>().OwnsOne<DepartmentInfo>(c => c.Department);
            builder.Entity<PlacedStudent>().OwnsOne<SemesterInfo>(c => c.PreferredSemester);

            // FORMS
            builder.Entity<CTEForm>().HasMany<TransferredCourseGroup>(c => c.TransferredCourseGroups);
            builder.Entity<CTEForm>().HasOne<Approval>(c => c.DeanApproval);
            builder.Entity<CTEForm>().HasOne<Approval>(c => c.ChairApproval);
            builder.Entity<CTEForm>().HasOne<Approval>(c => c.ExchangeCoordinatorApproval);
            builder.Entity<CTEForm>().HasOne<Approval>(c => c.FacultyOfAdministrationBoardApproval);

            builder.Entity<PreApprovalForm>().HasOne<Approval>(c => c.ExchangeCoordinatorApproval);
            builder.Entity<PreApprovalForm>().HasMany<RequestedCourseGroup>(c => c.RequestedCourseGroups);
            builder.Entity<PreApprovalForm>().HasOne<Approval>(c => c.FacultyAdministrationBoardApproval);

            builder.Entity<Student>().HasMany<CTEForm>(c => c.CTEForms);
            builder.Entity<Student>().HasMany<PreApprovalForm>(c => c.PreApprovalForms);

            builder.Entity<TransferredCourseGroup>().HasOne<ExemptedCourse>(c => c.ExemptedCourse);
            builder.Entity<TransferredCourseGroup>().HasMany<TransferredCourse>(c => c.TransferredCourses);

            builder.Entity<RequestedCourseGroup>().HasOne<ExemptedCourse>(c => c.RequestedExemptedCourse);
            builder.Entity<RequestedCourseGroup>().HasMany<RequestedCourse>(c => c.RequestedCourses);

            builder.Entity<PlacementTable>().OwnsOne<DepartmentInfo>(c => c.Department);

            builder.Entity<PlacedStudent>().OwnsOne<DepartmentInfo>(c => c.Department);
            builder.Entity<PlacedStudent>().OwnsOne<SemesterInfo>(c => c.PreferredSemester);

            builder.Entity<EquivalenceRequest>().HasOne<Approval>(c => c.InstructorApproval);
            builder.Entity<EquivalenceRequest>().HasOne<ExemptedCourse>(c => c.ExemptedCourse);
            builder.Entity<EquivalenceRequest>().Navigation(c => c.ExemptedCourse).AutoInclude();
            builder.Entity<EquivalenceRequest>().Navigation(c => c.InstructorApproval).AutoInclude();

            // builder.Entity<Student>().Property(m => m.Majors).HasConversion(
            //     v => string.Join(',', v),
            //     v => (ICollection<DepartmentInfo>)v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            // );

            // builder.Entity<Student>().Property(m => m.Minors).HasConversion(
            //     v => string.Join(',', v),
            //     v => (ICollection<DepartmentInfo>)v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            // );

            // ...

            builder.Entity<Student>().Property(m => m.PreferredSchools).HasConversion(
                v => string.Join(";|;", v),
                v => (ICollection<string>)v.Split(";|;", StringSplitOptions.RemoveEmptyEntries).ToList()
            );

            builder.Entity<PlacedStudent>().Property(m => m.PreferredSchools).HasConversion(
                v => string.Join(";|;'", v),
                v => (ICollection<string>)v.Split(";|;", StringSplitOptions.RemoveEmptyEntries).ToList()
            );

            // builder.Entity<Student>().Property(m => m.PreferredSemester).HasConversion(
            //     v => v.ToString(),
            //     v => new SemesterInfo(v.ToString())
            // );

            // No need for discriminator since we are using separate tables for each actor type
            // builder.Entity<DomainUser>().HasDiscriminator<string>("BaseRole").HasValue<Student>("Student");

            // builder.Entity<Student>().OwnsMany<DepartmentInfo>(m => m.Majors);

            // Seeding database with example users
            // var tempIdentity = new AppUser
            // {
            //     Id = Guid.NewGuid(),
            //     UserName = "22002901",
            //     Email = "haktan.bilen@ug.bilkent.edu.tr"
            // };

            // builder.Entity<AppUser>().HasData(tempIdentity);

            // builder.Entity<Student>().HasData(new Student()
            // {
            //     Id = Guid.NewGuid(),
            //     EntranceYear = 2020,
            //     Major = new DepartmentInfo { DepartmentName = Utilities.Enum.Department.ComputerEngineering, FacultyName = Utilities.Enum.Faculty.Engineering },
            //     Minors = null,
            //     CGPA = 3.34,
            //     ExchangeScore = 87.2,
            //     PreferredSchools = new List<string> { "École Polytechnique Fédérale (EPF)" },
            //     ExchangeSchool = "École Polytechnique Fédérale (EPF)",
            //     PreferredSemester = new SemesterInfo { Semester = Utilities.Enum.Semester.Fall, AcademicYear = "2022-2023" },
            //     CTEForms = new List<CTEForm>(),
            //     PreApprovalForms = new List<PreApprovalForm>(),
            //     EquivalenceRequestForms = new List<EquivalenceRequest>(),
            //     FirstName = "Borga Haktan",
            //     LastName = "Bilen",
            //     IdentityUser = tempIdentity
            // });
        }

        public DbSet<DomainUser> DomainUsers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<DeanDepartmentChair> DeanDepartmentChairs { get; set; }
        public DbSet<ExchangeCoordinator> ExchangeCoordinators { get; set; }
        public DbSet<CourseCoordinatorInstructor> CourseCoordinatorInstructors { get; set; }
        public DbSet<OISEP> OISEPs { get; set; }
        public DbSet<PlacementTable> PlacementTables { get; set; }
        public DbSet<PlacedStudent> PlacedStudents { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<CTEForm> CTEForms { get; set; }
        public DbSet<PreApprovalForm> PreApprovalForms { get; set; }
        public DbSet<EquivalenceRequest> EquivalenceRequests { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<LoggedEquivalentCourse> LoggedEquivalentCourses { get; set; }
        public DbSet<LoggedTransferredCourse> LoggedTransferredCourses { get; set; }
    }
}
