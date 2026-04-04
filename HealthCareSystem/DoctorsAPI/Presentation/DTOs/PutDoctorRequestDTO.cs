namespace HealthCareSystem.DoctorsAPI.Presentation.DTOs
{
    public record PutDoctorRequestDTO(
        Guid DoctorId,
        string FirstName,
        string LastName,
        string Specialty
    );
}
