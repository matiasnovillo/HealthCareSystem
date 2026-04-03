using HealthCareSystem.Infrastructure.ExternalServices.HttpClients.Doctor;

namespace HealthCareSystem.Application.Interfaces.Doctor
{
    public interface IDoctorService
    {
        Task<DoctorResponse> GetOneByIdAsync(Guid id);
    }
}
