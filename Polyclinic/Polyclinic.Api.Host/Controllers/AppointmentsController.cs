using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace Polyclinic.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int> service, ILogger<AppointmentsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все записи на прием
    /// </summary>
    /// <returns>Список всех записей на прием</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<AppointmentDto>> GetAll()
    {
        try
        {
            var appointments = service.GetAll();
            return Ok(appointments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all appointments");
            return Problem(
                title: "Unable to fetch appointments.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить запись на прием по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <returns>Запись на прием</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<AppointmentDto> Get(int id)
    {
        try
        {
            var appointment = service.Get(id);
            return Ok(appointment);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Appointment with ID {AppointmentId} not found", id);
            return NotFound($"Appointment with ID {id} not found");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting appointment with ID {AppointmentId}", id);
            return Problem(
                title: "Unable to fetch appointment.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Создать новую запись на прием
    /// </summary>
    /// <param name="dto">Данные для создания записи</param>
    /// <returns>Созданная запись</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<AppointmentDto> Create([FromBody] AppointmentCreateUpdateDto dto)
    {
        try
        {
            var createdAppointment = service.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = createdAppointment.Id }, createdAppointment);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating appointment");
            return Problem(
                title: "Unable to create appointment.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Обновить запись на прием
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="dto">Данные для обновления</param>
    /// <returns>Обновленная запись</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<AppointmentDto> Update(int id, [FromBody] AppointmentCreateUpdateDto dto)
    {
        try
        {
            var updatedAppointment = service.Update(id, dto);
            return Ok(updatedAppointment);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Appointment with ID {AppointmentId} not found for update", id);
            return NotFound($"Appointment with ID {id} not found");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating appointment with ID {AppointmentId}", id);
            return Problem(
                title: "Unable to update appointment.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Удалить запись на прием
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <returns>Результат операции</returns>
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
            logger.LogWarning(ex, "Appointment with ID {AppointmentId} not found for deletion", id);
            return NotFound($"Appointment with ID {id} not found");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting appointment with ID {AppointmentId}", id);
            return Problem(
                title: "Unable to delete appointment.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}