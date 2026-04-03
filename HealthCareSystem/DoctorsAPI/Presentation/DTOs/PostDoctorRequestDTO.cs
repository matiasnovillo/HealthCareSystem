namespace DoctorsAPI.Presentation.DTOs
{
    public record PostDoctorRequestDTO(
        string FirstName,
        string LastName,
        string Specialty
    );
}
