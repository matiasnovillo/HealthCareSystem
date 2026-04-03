namespace HealthCareSystem.Presentation.DTOs.Request
{
    public record PutAppointmentRequestDTO(
        Guid AppointmentId,
        Guid PatientId,
        Guid DoctorId,
        DateTime StartTime,
        DateTime EndTime,
        string RoomNumber,
        string Building,
        string Purpose
    );
}
