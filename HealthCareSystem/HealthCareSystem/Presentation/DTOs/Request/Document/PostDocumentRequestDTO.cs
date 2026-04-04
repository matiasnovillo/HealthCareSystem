namespace HealthCareSystem.AppointmentsAPI.Presentation.DTOs.Request.Document
{
    public record PostDocumentRequestDTO(
        Guid PatientId,
        string URL
    );
}
