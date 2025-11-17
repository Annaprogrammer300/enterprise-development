using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace Polyclinic.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int> service, ILogger<AppointmentsController> logger) : ControllerBase
{
    /// <summary>
    /// Get all appointment appointments
    /// </summary>
    /// <returns>List of all appointment appointments</returns>
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
    /// Get an appointment by ID
    /// </summary>
    /// <param name="id">Record ID</param>
    /// <returns>Make an appointment</returns>
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
    /// Create a new appointment
    /// </summary>
    /// <param name="dto">Data for creating a record</param>
    /// <returns>The created record</returns>
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
    /// Update the appointment
    /// </summary>
    /// <param name="id">Record ID</param>
    /// <param name="dto">Update data</param>
    /// <returns>Updated entry</returns>
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
    /// Delete an appointment
    /// </summary>
    /// <param name="id">Record ID</param>
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