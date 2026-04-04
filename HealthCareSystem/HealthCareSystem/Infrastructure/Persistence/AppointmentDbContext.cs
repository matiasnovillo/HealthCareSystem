using HealthCareSystem.AppointmentsAPI.Domain.Models;
using HealthCareSystem.AppointmentsAPI.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;

namespace HealthCareSystem.AppointmentsAPI.Infrastructure.Persistence
{
    public class AppointmentDbContext : DbContext
    {
        public DbSet<Appointment> Appointment { get; set; }

        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
                modelBuilder.Entity<Appointment>().ToTable("Appointment");
            }
            catch (Exception) { throw; }
        }
    }
}
