using Clinical.Application.InputModels;
using Clinical.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinical.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorsController(
            IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPatients()
        {
            try
            {
                var results = await _doctorService.GetAll();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while getting all doctors");
            }

        }

        [HttpGet("GetPaginatedDoctors")]
        public async Task<IActionResult> GetPaginatedDoctors([FromQuery] string? search, [FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            try
            {
                var results = await _doctorService.GetPaginatedDoctorsAsync(search ?? "", pageNumber, pageSize);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while getting doctors with pagination");
            }

        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] DoctorInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _doctorService.Add(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while adding doctor into database");
            }

        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] DoctorInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _doctorService.Update(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while updating doctor into database");
            }

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _doctorService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while deleting doctor into database");
            }

        }
    }
}
