using Microsoft.EntityFrameworkCore;
using WebApi.Configurations;
using WebApi.Data.Database.Models;

namespace WebApi.Data.Database
{
    public class EfCorePresentationContext : DbContext
    {
        public EfCorePresentationContext()
        {
        }

        public EfCorePresentationContext(DbContextOptions<EfCorePresentationContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($"Name=ConnectionStrings:{ApplicationSettings.EfCorePresentationConnectionString}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Roles)
                .WithMany(r => r.Employees)
                .UsingEntity<EmployeeRole>(
                    er => er
                        .HasOne(er => er.Role)
                        .WithMany(r => r.EmployeeRoles)
                        .HasForeignKey(er => er.RoleId),
                    er => er
                        .HasOne(er => er.Employee)
                        .WithMany(e => e.EmployeeRoles)
                        .HasForeignKey(er => er.EmployeeId),
                    er =>
                    {
                        er.HasKey(er => new { er.EmployeeId, er.RoleId });
                    });
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRole { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
