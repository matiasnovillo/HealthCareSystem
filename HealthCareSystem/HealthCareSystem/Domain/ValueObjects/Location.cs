using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem.AppointmentsAPI.Domain.ValueObjects
{
    [Owned]
    public record Location(string RoomNumber, string Building);
}
