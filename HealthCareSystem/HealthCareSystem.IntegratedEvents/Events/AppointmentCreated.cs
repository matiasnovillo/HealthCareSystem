namespace HealthCareSystem.IntegratedEvents.Events
{
    public class AppointmentCreated
    {
        public Guid MessageId { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
