using HealthCareSystem.Application.Interfaces.Doctor;
using HealthCareSystem.Application.Interfaces.Patient;
using HealthCareSystem.Infrastructure.ExternalServices.HttpClients.Doctor;
using HealthCareSystem.Infrastructure.ExternalServices.HttpClients.Patient;
using HealthCareSystem.IntegratedEvents.Events;
using MassTransit;
using System.Collections.Concurrent;

namespace HealthCareSystem.Infrastructure.Messaging.Consumers
{
    public class AppointmentCreatedConsumer(IPatientService _patientService, IDoctorService _doctorService) : IConsumer<AppointmentCreated>
    {
        // Dictionary to track the last processed timestamp for each appointment
        private static readonly ConcurrentDictionary<Guid, DateTime> LastProcessedTimestamps = new();
        private static readonly ConcurrentDictionary<Guid, bool> ProcessedMessageIds = new();

        public async Task Consume(ConsumeContext<AppointmentCreated> context)
        {
            AppointmentCreated AppointmentCreated = context.Message;

            if (ProcessedMessageIds.ContainsKey(AppointmentCreated.MessageId))
            {
                // Message has already been processed, ignore it
                Console.WriteLine($"Duplicate message detected, MessageId: {AppointmentCreated.MessageId}. Ignoring.");
                return;
            }

            // Retrieve the last processed timestamp for this appointment
            DateTime LastTimestamp = LastProcessedTimestamps.GetOrAdd(AppointmentCreated.AppointmentId, DateTime.MinValue);

            // Check if the message is newer than the last processed message
            if (AppointmentCreated.Timestamp > LastTimestamp)
            {

                Console.WriteLine($"Retrieve doctor details");

                DoctorResponse DoctorResponse = await _doctorService.GetOneByIdAsync(AppointmentCreated.DoctorId);;

                Console.WriteLine($"Retrieve patient details");

                PatientResponse PatientResponse = await _patientService.GetOneByIdAsync(AppointmentCreated.PatientId);

                Console.WriteLine($"Send Email to patient ");

                //Logic to send email to patient

                ProcessedMessageIds[AppointmentCreated.MessageId] = true;

            }
            else
            {
                //Implement logic to handle out-of-order messages, such as logging or storing for later processing
            }
        }
    }
}
