using HealthCareSystem.AppointmentsAPI.Infrastructure.ExternalServices.HttpClients.Doctor;

namespace HealthCareSystem.AppointmentsAPI.Application.Interfaces.Doctor
{
    public interface IDoctorService
    {
        Task<DoctorResponse> GetOneByIdAsync(Guid id);
    }
}
