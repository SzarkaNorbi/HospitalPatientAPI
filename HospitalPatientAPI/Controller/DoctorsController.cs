using HospitalPatientAPI.Context;
using HospitalPatientAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalPatientAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController(HospitalContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Doctor doctor)
        {
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, doctor);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await context.Doctors.ToListAsync();
            return Ok(doctors);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var doctor = await context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);
            if (doctor is null) { return NotFound(); }
            return Ok(doctor);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Doctor updatedDoctor)
        {
            var doctorToUpdate = await context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);
            if (doctorToUpdate is null) { return NotFound(); }
            doctorToUpdate.Name = updatedDoctor.Name;
            doctorToUpdate.Specialty = updatedDoctor.Specialty;
            doctorToUpdate.ContactInfo = updatedDoctor.ContactInfo;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var doctorToDelete = await context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);
            if (doctorToDelete is null) { return NotFound(); }

            context.Doctors.Remove(doctorToDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
