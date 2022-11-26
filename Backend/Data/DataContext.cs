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

            builder.Entity<Student>().Navigation(s => s.Majors).AutoInclude();
            builder.Entity<Student>().Navigation(s => s.Minors).AutoInclude();
            builder.Entity<Student>().Navigation(s => s.CTEForms).AutoInclude();
            builder.Entity<Student>().Navigation(s => s.PreApprovalForms).AutoInclude();
            builder.Entity<ExchangeCoordinator>().Navigation(s => s.Department).AutoInclude();

            builder.Entity<ExchangeCoordinator>().OwnsOne<DepartmentInfo>(c => c.Department);

            builder.Entity<Student>().OwnsMany<DepartmentInfo>(c => c.Majors);
            builder.Entity<Student>().OwnsMany<DepartmentInfo>(c => c.Minors);
            builder.Entity<Student>().OwnsMany<CTEForm>(c => c.CTEForms);
            builder.Entity<Student>().OwnsMany<PreApprovalForm>(c => c.PreApprovalForms);

            builder.Entity<Student>().OwnsOne<SemesterInfo>(c => c.PreferredSemester);

            // FORMS
            builder.Entity<CTEForm>().Navigation(c => c.TransferredCourseGroups).AutoInclude();
            builder.Entity<TransferredCourseGroup>().Navigation(c => c.TransferredCourses).AutoInclude();
            builder.Entity<PreApprovalForm>().Navigation(c => c.RequestedCourseGroups).AutoInclude();
            builder.Entity<RequestedCourseGroup>().Navigation(c => c.RequestedCourses).AutoInclude();

            builder.Entity<CTEForm>().OwnsOne<Approval>(c => c.DeanApproval);
            builder.Entity<CTEForm>().OwnsOne<Approval>(c => c.ChairApproval);
            builder.Entity<CTEForm>().OwnsOne<Approval>(c => c.ExchangeCoordinatorApproval);
            builder.Entity<CTEForm>().OwnsMany<TransferredCourseGroup>(c => c.TransferredCourseGroups);

            builder.Entity<TransferredCourseGroup>().OwnsMany<TransferredCourse>(c => c.TransferredCourses);
            builder.Entity<TransferredCourseGroup>().OwnsOne<ExemptedCourse>(c => c.ExemptedCourse);

            builder.Entity<PreApprovalForm>().OwnsMany<RequestedCourseGroup>(c => c.RequestedCourseGroups);

            builder.Entity<RequestedCourseGroup>().OwnsMany<RequestedCourse>(c => c.RequestedCourses);
            builder.Entity<RequestedCourseGroup>().OwnsOne<RequestedExemptedCourse>(c => c.RequestedExemptedCourse);

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
                v => string.Join(',', v),
                v => (ICollection<string>)v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
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
        public DbSet<ExchangeCoordinator> ExchangeCoordinators { get; set; }
    }
}
