using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Doctors;
using Microsoft.AspNetCore.Mvc;


namespace Polyclinic.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController(IApplicationService<DoctorDto, DoctorCreateUpdateDto, int> service, ILogger<DoctorsController> logger) : ControllerBase
{
    /// <summary>
    /// Get all doctors
    /// </summary>
    /// <returns>List of all doctors</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DoctorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<DoctorDto>> GetAll()
    {
        try
        {
            var doctors = service.GetAll();
            return Ok(doctors);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all doctors");
            return Problem(
                title: "Unable to fetch doctors.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Get a doctor by ID
    /// </summary>
    /// <param name="id">Doctor's ID</param>
    /// <returns>Doctor's data</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<DoctorDto> Get(int id)
    {
        try
        {
            var doctor = service.Get(id);
            return Ok(doctor);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Doctor with ID {DoctorId} not found", id);
            return NotFound($"Doctor with ID {id} not found");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting doctor with ID {DoctorId}", id);
            return Problem(
                title: "Unable to fetch doctor.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Create a new doctor
    /// </summary>
    /// <param name="dto">Data for creating a doctor</param>
    /// <returns>The created doctor</returns>
    [HttpPost]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<DoctorDto> Create([FromBody] DoctorCreateUpdateDto dto)
    {
        try
        {
            var createdDoctor = service.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = createdDoctor.Id }, createdDoctor);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating doctor");
            return Problem(
                title: "Unable to create doctor.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Update doctor
    /// </summary>
    /// <param name="id">Doctor's ID</param>
    /// <param name="dto">Update data</param>
    /// <returns>Updated doctor's data</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<DoctorDto> Update(int id, [FromBody] DoctorCreateUpdateDto dto)
    {
        try
        {
            var updatedDoctor = service.Update(id, dto);
            return Ok(updatedDoctor);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Doctor with ID {DoctorId} not found for update", id);
            return NotFound($"Doctor with ID {id} not found");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating doctor with ID {DoctorId}", id);
            return Problem(
                title: "Unable to update doctor.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Delete the doctor
    /// </summary>
    /// <param name="id">Doctor's ID</param>
    /// <returns>The result of the operation</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            service.Delete(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Doctor with ID {DoctorId} not found for deletion", id);
            return NotFound($"Doctor with ID {id} not found");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting doctor with ID {DoctorId}", id);
            return Problem(
                title: "Unable to delete doctor.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}