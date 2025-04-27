using Clinical.Application.InputModels;
using Clinical.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinical.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientsController(
            IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPatients()
        {
            try
            {
                var results = await _patientService.GetAllActivePatientsWithDoctor();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while getting all patients");
            }

        }

        [HttpGet("GetPaginatedPatients")]
        public async Task<IActionResult> GetPaginatedPatients([FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            try
            {
                var patients = await _patientService.GetPaginatedPatientsAsync(u => true, pageNumber, pageSize);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while getting patients with pagination");
            }

        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] PatientInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _patientService.AddPatient(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while adding patient into database");
            }

        }
    }
}
