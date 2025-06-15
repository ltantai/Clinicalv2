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
        public async Task<IActionResult> GetPaginatedPatients([FromQuery] string? search, [FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            try
            {
                var patients = await _patientService.GetPaginatedPatientsAsync(search ?? "", pageNumber, pageSize);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while getting patients with pagination");
            }

        }

        [HttpGet("GetPatientById/{id}")]
        public async Task<IActionResult> GetPatientById([FromRoute] int id)
        {
            try
            {
                var results = await _patientService.GetPatientById(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while getting patient detail");
            }

        }

        [HttpGet("GetPrescriptionDetail")]
        public async Task<IActionResult> GetPrescriptionDetail([FromQuery] int order, [FromQuery] int patientId)
        {
            try
            {
                var results = await _patientService.GetPrescriptionDetail(order, patientId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while getting all prescriptions");
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

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] PatientInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _patientService.UpdatePatient(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while adding patient into database");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Data Invalid");
                }
                await _patientService.DeletePatient(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while adding patient into database");
            }
        }

        [HttpPost("AddPresciptionForPatient")]
        public async Task<IActionResult> AddPresciptionForPatient([FromBody] PrescriptiondetailInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _patientService.AddPrescriptionForPatient(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while adding prescription for patient into database");
            }
        }
    }
}
