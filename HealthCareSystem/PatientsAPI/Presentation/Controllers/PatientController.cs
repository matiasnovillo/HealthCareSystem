using PatientsAPI.Domain.Models;
using PatientsAPI.Presentation.DTOs;
using HealthCareSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PatientsAPI.Presentation.Controllers
{
    [ApiController]
    public class PatientController(PatientDbContext _context) : ControllerBase
    {
        [HttpGet("api/Patient/GetAll")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAll()
        {
            return await _context.Patient.ToListAsync();
        }

        [HttpGet("api/Patient/GetOneByGUID/{id}")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetOneByGUID(Guid id)
        {
            Patient? Patient = await _context.Patient.FindAsync(id);

            if (Patient == null)
            {
                return NotFound();
            }

            return Ok(Patient);
        }

        [HttpPost("api/Patient/Post")]
        public async Task<ActionResult<Patient>> Post(PostPatientRequestDTO request)
        {
            try
            {
                Guid PatientId = Guid.NewGuid();

                Patient Patient = new(
                    PatientId,
                    request.FirstName,
                    request.LastName,
                    request.Email);

                _context.Patient.Add(Patient);

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOneByGUID), new { id = Patient.PatientId }, Patient);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("api/Patient/Put/{id}")]
        public async Task<ActionResult<Patient>> Put(Guid id, PutPatientRequestDTO request)
        {
            if (id != request.PatientId)
            {
                return BadRequest("ID in URL does not match ID in request body.");
            }

            Patient Patient = new(
                request.PatientId,
                request.FirstName,
                request.LastName,
                request.Email);

            _context.Entry(Patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool PatientExists = _context.Patient.Any(e => e.PatientId == id);

                if (!PatientExists)
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

        [HttpDelete("api/Patient/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Patient? Patient = await _context.Patient.FindAsync(id);

            if (Patient == null)
            {
                return NotFound();
            }
            _context.Patient.Remove(Patient);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
