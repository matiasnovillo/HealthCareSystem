namespace HealthCareSystem.AppointmentsAPI.Presentation.DTOs.Request.Appointment
{
    public record RescheduleAppointmentRequestDTO(
        DateTime NewStartTime,
        DateTime NewEndTime
    );
}
