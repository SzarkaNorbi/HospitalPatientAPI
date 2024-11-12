using HospitalPatientAPI.Context;
using HospitalPatientAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalPatientAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController(HospitalContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Patient patient)
        {
            context.Patients.Add(patient);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await context.Patients.ToListAsync();
            return Ok(patients);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await context.Patients.FirstOrDefaultAsync(patient => patient.Id == id);
            if (patient is null) { return NotFound(); }
            return Ok(patient);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Patient updatedPatient)
        {
            var patientToUpdate = await context.Patients.FirstOrDefaultAsync(patient => patient.Id == id);
            if (patientToUpdate is null) { return NotFound(); }
            patientToUpdate.Name = updatedPatient.Name;
            patientToUpdate.DateOfBirth = updatedPatient.DateOfBirth;
            patientToUpdate.Gender = updatedPatient.Gender;
            patientToUpdate.ContactInfo = updatedPatient.ContactInfo;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var patientToDelete = await context.Patients.FirstOrDefaultAsync(patient => patient.Id == id);
            if (patientToDelete is null) { return NotFound(); }

            context.Patients.Remove(patientToDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
