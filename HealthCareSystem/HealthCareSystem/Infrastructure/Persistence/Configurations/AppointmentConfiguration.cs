using HealthCareSystem.AppointmentsAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCareSystem.AppointmentsAPI.Infrastructure.Persistence.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> entity)
        {
            try
            {
                //AppointmentId
                entity.HasKey(e => e.AppointmentId);

                entity.OwnsOne(e => e.TimeSlot);

                entity.OwnsOne(e => e.Location);
            }
            catch (Exception) { throw; }
        }
    }
}
