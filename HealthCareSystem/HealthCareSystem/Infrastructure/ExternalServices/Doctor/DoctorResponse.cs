namespace HealthCareSystem.Infrastructure.ExternalServices.Doctor
{
    public record DoctorResponse(
        Guid DoctorId,
        string FirstName,
        string LastName,
        string Specialty
    );
}
