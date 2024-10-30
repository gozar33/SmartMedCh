using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMed.Application.Medications;
using SmartMed.Application.Medications.DTOs;

namespace SmartMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationAppService _medicationAppService;

        public MedicationController(IMedicationAppService medicationAppService)
        {
            _medicationAppService = medicationAppService;
        }

        [HttpGet("GetMedications")]
        public async Task<ActionResult<List<MedicationListDto>>> GetMedications()
        {
            return Ok(await _medicationAppService.GetAllAsync());
        }

        [HttpPost("CreateMedication")]
        public async Task<ActionResult> CreateMedication(MedicationDto medication)
        {
            await _medicationAppService.AddAsync(medication);
            return Created(nameof(CreateMedication), medication);
        }

        [HttpDelete("DeleteMedication/{id}")]
        public async Task<ActionResult> DeleteMedication(int id)
        {
            if (await _medicationAppService.DeleteAsync(id))
                return NoContent();

            return NotFound();
        }
    }
}
