using HealthCareSystem.AppointmentsAPI.Infrastructure.ExternalServices.HttpClients.Patient;

namespace HealthCareSystem.AppointmentsAPI.Application.Interfaces.Patient
{
    public interface IPatientService
    {
        Task<PatientResponse> GetOneByIdAsync(Guid id);
    }
}
