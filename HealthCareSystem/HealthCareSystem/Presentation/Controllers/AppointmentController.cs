using Grpc.Net.Client;
using HealthCareSystem.Application.Interfaces.Doctor;
using HealthCareSystem.Application.Interfaces.Patient;
using HealthCareSystem.Domain.Models;
using HealthCareSystem.Domain.ValueObjects;
using HealthCareSystem.Infrastructure.ExternalServices.gRPCClients.Document;
using HealthCareSystem.Infrastructure.ExternalServices.HttpClients.Doctor;
using HealthCareSystem.Infrastructure.ExternalServices.HttpClients.Patient;
using HealthCareSystem.Infrastructure.Persistence;
using HealthCareSystem.Presentation.DTOs.Request;
using HealthCareSystem.Presentation.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem.Presentation.Controllers
{
    [ApiController]
    public class AppointmentController(
        AppointmentDbContext _context, 
        IPatientService _patientService, 
        IDoctorService _doctorService, 
        IConfiguration _configuration) : ControllerBase
    {
        [HttpGet("api/Appointment/GetAll")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAll()
        {
            return await _context.Appointment.ToListAsync();
        }

        [HttpGet("api/Appointment/GetOneByGUID/{id}")]
        public async Task<ActionResult<Appointment>> GetOneByGUID(Guid id)
        {
            var appointment = await _context.Appointment.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        [HttpGet("api/Appointment/GetOneByGUID/{id}/Details")]
        public async Task<IActionResult> Details(Guid id)
        {
            Appointment? Appointment = await _context.Appointment.FindAsync(id);

            if (Appointment == null)
            {
                return NotFound();
            }

            //Http calls
            PatientResponse PatientResponse = await _patientService.GetOneByIdAsync(Appointment.PatientId);
            DoctorResponse DoctorResponse = await _doctorService.GetOneByIdAsync(Appointment.DoctorId);

            //gRPC calls
            using GrpcChannel GrpcChannel = GrpcChannel.ForAddress(_configuration["GrpcEndpoints:DocumentService"]);
            var Client = new DocumentService.DocumentServiceClient(GrpcChannel);
            var documents = await Client.GetAllAsync(new PatientId { Id = PatientResponse.PatientId.ToString() });

            AppointmentDetailsDTO AppointmentDetailsDTO = new(
                id,
                DoctorResponse,
                PatientResponse,
                Appointment.TimeSlot.Start,
                Appointment.TimeSlot.End,
                Appointment.Location.RoomNumber,
                Appointment.Location.Building,
                Appointment.Purpose,
                documents
                );

            return Ok(AppointmentDetailsDTO);
        }

        [HttpPost("api/Appointment/Post")]
        public async Task<ActionResult<Appointment>> Post(PostAppointmentRequestDTO request)
        {
            try
            {
                TimeSlot TimeSlot = new(request.StartTime, request.EndTime);

                Location Location = new(request.RoomNumber, request.Building);

                Guid AppointmentId = Guid.NewGuid();

                Appointment Appointment = new(
                    AppointmentId,
                    request.PatientId,
                    request.DoctorId,
                    TimeSlot,
                    Location,
                    request.Purpose
                );

                _context.Appointment.Add(Appointment);

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOneByGUID), new { id = Appointment.AppointmentId }, Appointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("api/Appointment/Put/{id}")]
        public async Task<IActionResult> Put(Guid id, PutAppointmentRequestDTO request)
        {
            if (id != request.AppointmentId)
            {
                return BadRequest("ID in URL does not match ID in request body.");
            }

            TimeSlot TimeSlot = new(request.StartTime, request.EndTime);
            
            Location Location = new(request.RoomNumber, request.Building);

            Appointment Appointment = new(
                request.AppointmentId,
                request.PatientId,
                request.DoctorId,
                TimeSlot,
                Location,
                request.Purpose
            );

            _context.Entry(Appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool AppointmentExists = _context.Appointment.Any(e => e.AppointmentId == id);

                if (!AppointmentExists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("api/Appointment/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Appointment? Appointment = await _context.Appointment.FindAsync(id);

            if (Appointment == null)
            {
                return NotFound();
            }

            _context.Appointment.Remove(Appointment);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
