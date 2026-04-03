namespace DoctorsAPI.Domain.Models
{
    public class Doctor
    {
        public Guid DoctorId { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Specialty { get; private set; } = string.Empty;

        // EF Core constructor
        private Doctor() { }

        public Doctor(Guid doctorId, string firstName, string lastName, string specialty)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            }
            if (string.IsNullOrWhiteSpace(specialty))
            {
                throw new ArgumentException("Specialty cannot be empty.", nameof(specialty));
            }

            DoctorId = doctorId;
            FirstName = firstName;
            LastName = lastName;
            Specialty = specialty;
        }
    }
}
