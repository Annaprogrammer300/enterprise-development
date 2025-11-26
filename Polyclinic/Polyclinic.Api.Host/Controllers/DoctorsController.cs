using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Doctors;
using Microsoft.AspNetCore.Mvc;

namespace Polyclinic.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController(IApplicationService<DoctorDto, DoctorCreateUpdateDto, int> service) : ControllerBase
{
    /// <summary>
    /// Get all doctors
    /// </summary>
    /// <returns>List of all doctors</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DoctorDto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<DoctorDto>> GetAll()
    {
        var doctors = service.GetAll();
        return Ok(doctors);
    }

    /// <summary>
    /// Get a doctor by ID
    /// </summary>
    /// <param name="id">Doctor's ID</param>
    /// <returns>Doctor's data</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DoctorDto> Get(int id)
    {
        try
        {
            var doctor = service.Get(id);
            return Ok(doctor);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Doctor with id {id} not found");
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
    public ActionResult<DoctorDto> Create([FromBody] DoctorCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdDoctor = service.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = createdDoctor.Id }, createdDoctor);
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
    public ActionResult<DoctorDto> Update(int id, [FromBody] DoctorCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedDoctor = service.Update(id, dto);
            return Ok(updatedDoctor);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Doctor with id {id} not found");
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
    public IActionResult Delete(int id)
    {
        try
        {
            service.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Doctor with id {id} not found");
        }
    }
}