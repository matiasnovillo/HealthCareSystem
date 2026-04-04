using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HealthCareSystem.PatientsAPI.Domain.Models;

namespace HealthCareSystem.PatientsAPI.Infrastructure.Persistence.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> entity)
        {
            try
            {
                //PatientId
                entity.HasKey(e => e.PatientId);
            }
            catch (Exception) { throw; }
        }
    }
}
