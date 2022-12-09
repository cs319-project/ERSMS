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
    public class DataContext : IdentityDbContext<AppUser, Role, Guid>
    {

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DomainUser>().Navigation(u => u.IdentityUser).AutoInclude();
            builder.Entity<AppUser>().Navigation(u => u.DomainUser).AutoInclude();

            builder.Entity<CTEForm>().Navigation(c => c.TransferredCourseGroups).AutoInclude();
            builder.Entity<CTEForm>().Navigation(c => c.DeanApproval).AutoInclude();
            builder.Entity<CTEForm>().Navigation(c => c.ChairApproval).AutoInclude();
            builder.Entity<CTEForm>().Navigation(c => c.ExchangeCoordinatorApproval).AutoInclude();
            // builder.Entity<CTEForm>().Navigation(c => c.SubjectStudent).AutoInclude();
            builder.Entity<TransferredCourseGroup>().Navigation(c => c.TransferredCourses).AutoInclude();
            builder.Entity<TransferredCourseGroup>().Navigation(c => c.ExemptedCourse).AutoInclude();
            builder.Entity<PreApprovalForm>().Navigation(c => c.RequestedCourseGroups).AutoInclude();
            builder.Entity<PreApprovalForm>().Navigation(c => c.ExchangeCoordinatorApproval).AutoInclude();
            // builder.Entity<PreApprovalForm>().Navigation(c => c.SubjectStudent).AutoInclude();
            builder.Entity<RequestedCourseGroup>().Navigation(c => c.RequestedCourses).AutoInclude();
            builder.Entity<RequestedCourseGroup>().Navigation(c => c.RequestedExemptedCourse).AutoInclude();

            builder.Entity<ExemptionRequestForm>().Navigation(c => c.SubjectStudent).AutoInclude();

            builder.Entity<Student>().Navigation(s => s.Major).AutoInclude();
            builder.Entity<Student>().Navigation(s => s.Minors).AutoInclude();
            builder.Entity<Student>().Navigation(s => s.CTEForms).AutoInclude();
            builder.Entity<Student>().Navigation(s => s.PreApprovalForms).AutoInclude();
            builder.Entity<Student>().Navigation(s => s.ExemptionRequestForms).AutoInclude();

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
            // builder.Entity<CTEForm>().HasOne<Student>(c => c.SubjectStudent);
            // builder.Entity<PreApprovalForm>().HasOne<Student>(c => c.SubjectStudent);
            builder.Entity<ExemptionRequestForm>().HasOne<Student>(c => c.SubjectStudent);

            builder.Entity<CTEForm>().HasMany<TransferredCourseGroup>(c => c.TransferredCourseGroups);
            builder.Entity<CTEForm>().HasOne<Approval>(c => c.DeanApproval);
            builder.Entity<CTEForm>().HasOne<Approval>(c => c.ChairApproval);
            builder.Entity<CTEForm>().HasOne<Approval>(c => c.ExchangeCoordinatorApproval);

            builder.Entity<PreApprovalForm>().HasOne<Approval>(c => c.ExchangeCoordinatorApproval);
            builder.Entity<PreApprovalForm>().HasMany<RequestedCourseGroup>(c => c.RequestedCourseGroups);

            builder.Entity<Student>().HasMany<CTEForm>(c => c.CTEForms);
            builder.Entity<Student>().HasMany<ExemptionRequestForm>(c => c.ExemptionRequestForms);
            builder.Entity<Student>().HasMany<PreApprovalForm>(c => c.PreApprovalForms);

            builder.Entity<TransferredCourseGroup>().HasOne<ExemptedCourse>(c => c.ExemptedCourse);
            builder.Entity<TransferredCourseGroup>().HasMany<TransferredCourse>(c => c.TransferredCourses);

            builder.Entity<RequestedCourseGroup>().HasOne<ExemptedCourse>(c => c.RequestedExemptedCourse);
            builder.Entity<RequestedCourseGroup>().HasMany<RequestedCourse>(c => c.RequestedCourses);

            builder.Entity<PlacementTable>().OwnsOne<DepartmentInfo>(c => c.Department);

            builder.Entity<PlacedStudent>().OwnsOne<DepartmentInfo>(c => c.Department);
            builder.Entity<PlacedStudent>().OwnsOne<SemesterInfo>(c => c.PreferredSemester);



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
        public DbSet<CTEForm> CTEForms { get; set; }
        public DbSet<PreApprovalForm> PreApprovalForms { get; set; }
    }
}
