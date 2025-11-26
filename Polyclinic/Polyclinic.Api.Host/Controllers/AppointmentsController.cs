using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<IEnumerable<AppointmentDto>> GetAll()
    {
        var appointments = service.GetAll();
        return Ok(appointments);
    }

    /// <summary>
    /// Get an appointment by ID
    /// </summary>
    /// <param name="id">Record ID</param>
    /// <returns>Make an appointment</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    }

    /// <summary>
    /// Create a new appointment
    /// </summary>
    /// <param name="dto">Data for creating a record</param>
    /// <returns>The created record</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<AppointmentDto> Create([FromBody] AppointmentCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdAppointment = service.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = createdAppointment.Id }, createdAppointment);
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
        catch (KeyNotFoundException)
        {
            return NotFound($"Appointment with id {id} not found");
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
    }
}