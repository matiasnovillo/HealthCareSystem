namespace HealthCareSystem.Presentation.DTOs.Request.Document
{
    public record PostDocumentRequestDTO(
        Guid PatientId,
        string URL
    );
}
