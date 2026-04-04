namespace HealthCareSystem.Presentation.DTOs.Request.Document
{
    public record PutDocumentRequestDTO(
        Guid DocumentId,
        Guid PatientId,
        string URL
        );
}
