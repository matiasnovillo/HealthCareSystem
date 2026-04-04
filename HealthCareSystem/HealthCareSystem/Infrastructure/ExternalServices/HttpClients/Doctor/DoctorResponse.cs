namespace HealthCareSystem.AppointmentsAPI.Infrastructure.ExternalServices.HttpClients.Doctor
{
    public record DoctorResponse(
        Guid DoctorId,
        string FirstName,
        string LastName,
        string Specialty
    );
}
