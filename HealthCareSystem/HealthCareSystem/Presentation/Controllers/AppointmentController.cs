using HealthCareSystem.Domain.Models;
using HealthCareSystem.Domain.ValueObjects;
using HealthCareSystem.Infrastructure.Persistence;
using HealthCareSystem.Presentation.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem.Presentation.Controllers
{
    [ApiController]
    public class AppointmentController(AppointmentDbContext _context) : ControllerBase
    {
        [HttpGet("api/Appointment/GetAll")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAll()
        {
            return await _context.Appointment.ToListAsync();
        }

        [HttpGet("api/Appointment/GetOneByGUID/{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(Guid id)
        {
            var appointment = await _context.Appointment.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        [HttpPost("api/Appointment/Post")]
        public async Task<ActionResult<Appointment>> PostAppointment(CreateAppointmentRequestDTO request)
        {
            try
            {
                TimeSlot TimeSlot = new(request.StartTime, request.EndTime);

                Location Location = new(request.RoomNumber, request.Building);

                Guid appointmentId = Guid.NewGuid();

                Appointment Appointment = new(
                    appointmentId,
                    request.PatientId,
                    request.DoctorId,
                    TimeSlot,
                    Location,
                    request.Purpose
                );

                _context.Appointment.Add(Appointment);

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAppointment", new { id = Appointment.AppointmentId }, Appointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
