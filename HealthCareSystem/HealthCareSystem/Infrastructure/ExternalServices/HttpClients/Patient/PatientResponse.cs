namespace HealthCareSystem.Infrastructure.ExternalServices.HttpClients.Patient
{
    public record PatientResponse(
        Guid PatientId,
        string FirstName,
        string LastName,
        string Email
    );
}
