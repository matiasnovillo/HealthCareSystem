namespace HealthCareSystem.Presentation.DTOs.Request
{
    public record RescheduleAppointmentRequestDTO(
        DateTime NewStartTime,
        DateTime NewEndTime
    );
}
