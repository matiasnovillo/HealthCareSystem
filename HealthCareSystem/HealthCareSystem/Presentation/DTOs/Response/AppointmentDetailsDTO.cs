using HealthCareSystem.Infrastructure.ExternalServices.Doctor;
using HealthCareSystem.Infrastructure.ExternalServices.Patient;

namespace HealthCareSystem.Presentation.DTOs.Response
{
    public record AppointmentDetailsDTO(
        Guid AppointmentId,
        DoctorResponse Doctor,
        PatientResponse Patient,
        DateTime StartTime,
        DateTime EndTime,
        string RoomNumber,
        string Building,
        string Purpose
        );
}
