using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem.AppointmentsAPI.Domain.ValueObjects
{
    [Owned]
    public record TimeSlot
    {
        public DateTime Start { get; init; }

        public DateTime End { get; init; }

        public TimeSpan Duration => End - Start;

        public TimeSlot(DateTime start, DateTime end)
        {
            if (end <= start)
            {
                throw new ArgumentException("End time must be after start time.");
            }

            Start = start;
            End = end;
        }
    }
}
