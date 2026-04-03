namespace PatientsAPI.Presentation.DTOs
{
    public record PutPatientRequestDTO(
        Guid PatientId,
        string FirstName,
        string LastName,
        string Email
    );
}
