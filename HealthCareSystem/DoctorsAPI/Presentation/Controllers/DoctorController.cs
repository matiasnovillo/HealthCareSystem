using DoctorsAPI.Domain.Models;
using DoctorsAPI.Infrastructure.Persistence;
using DoctorsAPI.Presentation.DTOs;
using HealthCareSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorsAPI.Presentation.Controllers
{
    [ApiController]
    public class DoctorController(DoctorDbContext _context) : ControllerBase
    {
        [HttpGet("api/Doctor/GetAll")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAll()
        {
            return await _context.Doctor.ToListAsync();
        }

        [HttpGet("api/Doctor/GetOneByGUID/{id}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetOneByGUID(Guid id)
        {
            Doctor? Doctor = await _context.Doctor.FindAsync(id);

            if (Doctor == null)
            {
                return NotFound();
            }

            return Ok(Doctor);
        }

        [HttpPost("api/Doctor/Post")]
        public async Task<ActionResult<Doctor>> Post(PostDoctorRequestDTO request)
        {
            try
            {
                Guid DoctorId = Guid.NewGuid();

                Doctor Doctor = new(
                    DoctorId,
                    request.FirstName,
                    request.LastName,
                    request.Specialty);

                _context.Doctor.Add(Doctor);

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOneByGUID), new { id = Doctor.DoctorId }, Doctor);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("api/Doctor/Put/{id}")]
        public async Task<ActionResult<Doctor>> Put(Guid id, PutDoctorRequestDTO request)
        {
            if (id != request.DoctorId)
            {
                return BadRequest("ID in URL does not match ID in request body.");
            }

            Doctor Doctor = new(
                request.DoctorId,
                request.FirstName,
                request.LastName,
                request.Specialty);

            _context.Entry(Doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool DoctorExists = _context.Doctor.Any(e => e.DoctorId == id);

                if (!DoctorExists)
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

        [HttpDelete("api/Doctor/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Doctor? Doctor = await _context.Doctor.FindAsync(id);

            if (Doctor == null)
            {
                return NotFound();
            }
            _context.Doctor.Remove(Doctor);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
