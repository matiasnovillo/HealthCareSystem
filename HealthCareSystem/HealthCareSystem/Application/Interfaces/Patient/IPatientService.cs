using HealthCareSystem.Infrastructure.ExternalServices.Patient;

namespace HealthCareSystem.Application.Interfaces.Patient
{
    public interface IPatientService
    {
        Task<PatientResponse> GetOneByIdAsync(Guid id);
    }
}
