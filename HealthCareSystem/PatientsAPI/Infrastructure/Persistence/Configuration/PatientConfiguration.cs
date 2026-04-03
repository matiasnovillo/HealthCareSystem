using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientsAPI.Domain.Models;

namespace PatientsAPI.Infrastructure.Persistence.Configurations
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
