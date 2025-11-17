using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Patients;
using Microsoft.AspNetCore.Mvc;

namespace Polyclinic.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IApplicationService<PatientDto, PatientCreateUpdateDto, int> service, ILogger<PatientsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить всех пациентов
    /// </summary>
    /// <returns>Список всех пациентов</returns>
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
    /// Получить пациента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пациента</param>
    /// <returns>Данные пациента</returns>
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
    /// Создать нового пациента
    /// </summary>
    /// <param name="dto">Данные для создания пациента</param>
    /// <returns>Созданный пациент</returns>
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
    /// Обновить данные пациента
    /// </summary>
    /// <param name="id">Идентификатор пациента</param>
    /// <param name="dto">Данные для обновления</param>
    /// <returns>Обновленные данные пациента</returns>
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
    /// Удалить пациента
    /// </summary>
    /// <param name="id">Идентификатор пациента</param>
    /// <returns>Результат операции</returns>
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