using HealthCareSystem.Infrastructure.ExternalServices.gRPCClients.Document;
using HealthCareSystem.Infrastructure.ExternalServices.HttpClients.Doctor;
using HealthCareSystem.Infrastructure.ExternalServices.HttpClients.Patient;

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
        string Purpose,
        DocumentList DocumentList
        );
}
