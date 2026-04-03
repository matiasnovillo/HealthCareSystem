namespace HealthCareSystem.Infrastructure.ExternalServices.Patient
{
    public record PatientResponse(
        Guid PatientId,
        string FirstName,
        string LastName,
        string Email
    );
}
