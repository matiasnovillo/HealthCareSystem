using HealthCareSystem.Domain.ValueObjects;

namespace HealthCareSystem.Domain.Models
{
    public class Appointment
    {
        public Guid AppointmentId { get; private set; }
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public TimeSlot TimeSlot { get; private set; } = null!;
        public Location Location { get; private set; } = null!;
        public string Purpose { get; private set; } = string.Empty;

        // EF Core constructor
        private Appointment() { }

        public Appointment(Guid appointmentId, Guid patientId, Guid doctorId, TimeSlot timeSlot, Location location, string purpose)
        {
            if (string.IsNullOrWhiteSpace(purpose))
            {
                throw new ArgumentException("Purpose cannot be empty.", nameof(purpose));
            }

            if (location == null)
            {
                throw new ArgumentNullException(nameof(location), "Location cannot be null.");
            }

            if (timeSlot == null)
            {
                throw new ArgumentNullException(nameof(timeSlot), "Time slot cannot be null.");
            }

            if (timeSlot.Start < DateTime.Now)
            {
                throw new ArgumentException("Time slot must be in the future.", nameof(timeSlot));
            }

            AppointmentId = appointmentId;
            PatientId = patientId;
            DoctorId = doctorId;
            TimeSlot = timeSlot;
            Location = location;
            Purpose = purpose;
        }

        /// <summary>
        /// Domain behavior: Reschedule the appointment to a new time slot
        /// </summary>
        public void Reschedule(TimeSlot newTimeSlot)
        {
            TimeSlot = newTimeSlot;
        }

        /// <summary>
        /// Domain behavior: Relocate the appointment to a new location
        /// </summary>
        public void Relocation(Location newLocation)
        {
            Location = newLocation;
        }

        /// <summary>
        /// Domain behavior: Change the purpose of the appointment
        /// </summary>
        public void ChangePurpose(string newPurpose)
        {
            Purpose = newPurpose;
        }
    }
}
