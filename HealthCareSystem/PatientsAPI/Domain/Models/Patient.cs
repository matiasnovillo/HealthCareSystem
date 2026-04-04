namespace HealthCareSystem.PatientsAPI.Domain.Models
{
    public class Patient
    {
        public Guid PatientId { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        // EF Core constructor
        private Patient() { }

        public Patient(Guid patientId, string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            }

            PatientId = patientId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void UpdateEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
            {
                throw new ArgumentException("Email cannot be empty.", nameof(newEmail));
            }

            Email = newEmail;
        }
    }
}
