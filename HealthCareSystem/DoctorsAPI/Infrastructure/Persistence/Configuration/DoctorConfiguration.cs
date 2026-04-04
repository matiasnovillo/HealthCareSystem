using HealthCareSystem.DoctorsAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCareSystem.DoctorsAPI.Infrastructure.Persistence.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> entity)
        {
            try
            {
                //DoctorId
                entity.HasKey(e => e.DoctorId);
            }
            catch (Exception) { throw; }
        }
    }
}
