using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;

namespace Polyclinic.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int> service) : ControllerBase
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving appointments: {ex.Message}");
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<AppointmentDto> Get(int id)
    {
        try
        {
            var appointment = service.Get(id);
            return Ok(appointment);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Appointment with id {id} not found");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Invalid request: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving appointment: {ex.Message}");
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdAppointment = service.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = createdAppointment.Id }, createdAppointment);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound($"Referenced entity not found: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Invalid data: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return Conflict($"Conflict: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error creating appointment: {ex.Message}");
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedAppointment = service.Update(id, dto);
            return Ok(updatedAppointment);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound($"Appointment or referenced entity not found: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Invalid data: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return Conflict($"Conflict: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error updating appointment: {ex.Message}");
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            service.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Appointment with id {id} not found");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Invalid request: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return Conflict($"Cannot delete appointment: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error deleting appointment: {ex.Message}");
        }
    }
}