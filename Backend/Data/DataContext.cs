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

            builder.Entity<Student>().Property(m => m.Majors).HasConversion(
                v => string.Join(',', v),
                v => (ICollection<DepartmentInfo>)v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

            builder.Entity<Student>().Property(m => m.Minors).HasConversion(
                v => string.Join(',', v),
                v => (ICollection<DepartmentInfo>)v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

            builder.Entity<Student>().Property(m => m.PreferredSchools).HasConversion(
                v => string.Join(',', v),
                v => (ICollection<string>)v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

            builder.Entity<Student>().Property(m => m.PreferredSemester).HasConversion(
                v => v.ToString(),
                v => new SemesterInfo(v.ToString())
            );

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
