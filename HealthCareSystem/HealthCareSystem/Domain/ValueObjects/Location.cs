using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem.Domain.ValueObjects
{
    [Owned]
    public record Location(string RoomNumber, string Building);
}
