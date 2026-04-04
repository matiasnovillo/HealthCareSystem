namespace HealthCareSystem.PatientsAPI.Presentation.DTOs
{
    public record PostPatientRequestDTO(
        string FirstName,
        string LastName,
        string Email
    );
}
