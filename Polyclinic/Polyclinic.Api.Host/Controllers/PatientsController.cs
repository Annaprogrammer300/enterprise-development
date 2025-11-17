using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Patients;
using Microsoft.AspNetCore.Mvc;

namespace Polyclinic.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IApplicationService<PatientDto, PatientCreateUpdateDto, int> service, ILogger<PatientsController> logger) : ControllerBase
{
    /// <summary>
    /// Get all patients
    /// </summary>
    /// <returns>List of all patients</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PatientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<PatientDto>> GetAll()
    {
        try
        {
            var patients = service.GetAll();
            return Ok(patients);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all patients");
            return Problem(
                title: "Unable to fetch patients.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Get a patient by ID
    /// </summary>
    /// <param name="id">Patient ID</param>
    /// <returns>Patient data</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<PatientDto> Get(int id)
    {
        try
        {
            var patient = service.Get(id);
            return Ok(patient);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Patient with ID {PatientId} not found", id);
            return NotFound($"Patient with ID {id} not found");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting patient with ID {PatientId}", id);
            return Problem(
                title: "Unable to fetch patient.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Create a new patient
    /// </summary>
    /// <param name="dto">Patient creation data</param>
    /// <returns>The created patient</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<PatientDto> Create([FromBody] PatientCreateUpdateDto dto)
    {
        try
        {
            var createdPatient = service.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = createdPatient.Id }, createdPatient);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating patient");
            return Problem(
                title: "Unable to create patient.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Update patient data
    /// </summary>
    /// <param name="id">Patient ID</param>
    /// <param name="dto">Update data</param>
    /// <returns>Updated patient data</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<PatientDto> Update(int id, [FromBody] PatientCreateUpdateDto dto)
    {
        try
        {
            var updatedPatient = service.Update(id, dto);
            return Ok(updatedPatient);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Patient with ID {PatientId} not found for update", id);
            return NotFound($"Patient with ID {id} not found");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating patient with ID {PatientId}", id);
            return Problem(
                title: "Unable to update patient.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Delete a patient
    /// </summary>
    /// <param name="id">Patient ID</param>
    /// <returns>The result of the operation</returns>
    [HttpDelete("{id:int}")]
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
            logger.LogWarning(ex, "Patient with ID {PatientId} not found for deletion", id);
            return NotFound($"Patient with ID {id} not found");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting patient with ID {PatientId}", id);
            return Problem(
                title: "Unable to delete patient.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}