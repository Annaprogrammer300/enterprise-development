using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Patients;

namespace Polyclinic.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IApplicationService<PatientDto, PatientCreateUpdateDto, int> service) : ControllerBase
{
    /// <summary>
    /// Get all patients
    /// </summary>
    /// <returns>List of all patients</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PatientDto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PatientDto>> GetAll()
    {
        var patients = service.GetAll();
        return Ok(patients);
    }

    /// <summary>
    /// Get a patient by ID
    /// </summary>
    /// <param name="id">Patient ID</param>
    /// <returns>Patient data</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PatientDto> Get(int id)
    {
        try
        {
            var patient = service.Get(id);
            return Ok(patient);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Patient with id {id} not found");
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
    public ActionResult<PatientDto> Create([FromBody] PatientCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdPatient = service.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = createdPatient.Id }, createdPatient);
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
    public ActionResult<PatientDto> Update(int id, [FromBody] PatientCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedPatient = service.Update(id, dto);
            return Ok(updatedPatient);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Patient with id {id} not found");
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
    public IActionResult Delete(int id)
    {
        try
        {
            service.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Patient with id {id} not found");
        }
    }
}