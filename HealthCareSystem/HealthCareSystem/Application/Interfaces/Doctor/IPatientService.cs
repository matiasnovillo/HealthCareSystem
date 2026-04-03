using HealthCareSystem.Infrastructure.ExternalServices.Doctor;

namespace HealthCareSystem.Application.Interfaces.Doctor
{
    public interface IDoctorService
    {
        Task<DoctorResponse> GetOneByIdAsync(Guid id);
    }
}
