using HealthCareSystem.AppointmentsAPI.Infrastructure.ExternalServices.gRPCClients.Document;
using HealthCareSystem.AppointmentsAPI.Infrastructure.ExternalServices.HttpClients.Doctor;
using HealthCareSystem.AppointmentsAPI.Infrastructure.ExternalServices.HttpClients.Patient;

namespace HealthCareSystem.AppointmentsAPI.Presentation.DTOs.Response
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
