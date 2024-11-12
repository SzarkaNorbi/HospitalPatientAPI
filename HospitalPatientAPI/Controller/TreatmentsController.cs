using Castle.Core.Resource;
using HospitalPatientAPI.Context;
using HospitalPatientAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalPatientAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentsController(HospitalContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Treatment treatment)
        {
            context.Treatments.Add(treatment);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTreatment), new { id = treatment.Id }, treatment);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetTreatments()
        {
            var treatments = await context.Treatments.ToListAsync();
            return Ok(treatments);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetTreatment(int id)
        {
            var treatment = await context.Treatments.FirstOrDefaultAsync(treatment => treatment.Id == id);
            if (treatment is null) { return NotFound(); }
            return Ok(treatment);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Treatment updatedTreatment)
        {
            var treatmentToUpdate = await context.Treatments.FirstOrDefaultAsync(treatment => treatment.Id == id);
            if (treatmentToUpdate is null) { return NotFound(); }
            treatmentToUpdate.PatientId = updatedTreatment.PatientId;
            treatmentToUpdate.DoctorId = updatedTreatment.DoctorId;
            treatmentToUpdate.TreatmentDetails = updatedTreatment.TreatmentDetails;
            treatmentToUpdate.StartDate = updatedTreatment.StartDate;
            treatmentToUpdate.EndDate = updatedTreatment.EndDate;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var treatmentToDelete = await context.Treatments.FirstOrDefaultAsync(treatment => treatment.Id == id);
            if (treatmentToDelete is null) { return NotFound(); }

            context.Treatments.Remove(treatmentToDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
