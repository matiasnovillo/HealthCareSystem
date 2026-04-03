using Microsoft.EntityFrameworkCore;
using PatientsAPI.Domain.Models;
using PatientsAPI.Infrastructure.Persistence.Configurations;

namespace HealthCareSystem.Infrastructure.Persistence
{
    public class PatientDbContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }

        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfiguration(new PatientConfiguration());
                modelBuilder.Entity<Patient>().ToTable("Patient");
            }
            catch (Exception) { throw; }
        }
    }
}
