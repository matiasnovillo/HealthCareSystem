using DoctorsAPI.Domain.Models;
using HealthCareSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DoctorsAPI.Infrastructure.Persistence
{
    public class DoctorDbContext : DbContext
    {
        public DbSet<Doctor> Doctor { get; set; }

        public DoctorDbContext(DbContextOptions<DoctorDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfiguration(new DoctorConfiguration());
                modelBuilder.Entity<Doctor>().ToTable("Doctor");
            }
            catch (Exception) { throw; }
        }
    }
}
