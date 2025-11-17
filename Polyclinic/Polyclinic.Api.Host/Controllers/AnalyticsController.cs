using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace Polyclinic.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Get a list of doctors with at least the specified number of years of experience
    /// </summary>
    /// <param name="minExperience">Minimum work experience in years</param>
    /// <returns>List of doctor IDs</returns>
    [HttpGet("doctors/experience/{minExperience}")]
    public ActionResult<List<int>> GetDoctorsWithExperienceAtLeast(int minExperience)
    {
        try
        {
            var result = analyticsService.GetDoctorsWithExperienceAtLeast(minExperience);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving doctors with experience at least {MinExperience} years", minExperience);
            return BadRequest($"An error occurred while processing the request: {ex.Message}");
        }
    }

    /// <summary>
    /// Get a list of patients by doctor ID
    /// </summary>
    /// <param name="DoctorID">Doctor's ID</param>
    /// <returns>List of patients</returns>
    [HttpGet("patients/by-doctor/{doctorId}")]
    public ActionResult<List<string>> GetPatientsByDoctor(int doctorId)
    {
        try
        {
            var result = analyticsService.GetPatientsByDoctor(doctorId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving patients for doctor {DoctorId}", doctorId);
            return BadRequest($"An error occurred while processing the request: {ex.Message}");
        }
    }

    /// <summary>
    /// Get the number of repeat appointments in the last month
    /// </summary>
    /// <param name="lastMonth">Start of the period (last month)</param>
    /// <param name="today">End of the period (today)</param>
    /// <returns>Number of repeat appointments</returns>
    [HttpGet("appointments/repeated-count")]
    public ActionResult<int> GetRepeatedAppointmentsCount([FromQuery] DateTime lastMonth, [FromQuery] DateTime today)
    {
        try
        {
            var result = analyticsService.CountRepeatedAppointmentsLastMonth(lastMonth, today);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error counting repeated appointments from {LastMonth} to {Today}", lastMonth, today);
            return BadRequest($"An error occurred while processing the request: {ex.Message}");
        }
    }

    /// <summary>
    /// Get a list of dates for patients over the specified age with multiple doctors
    /// </summary>
    /// <param name="age">Minimum age</param>
    /// <param name="date">The date for calculating the age</param>
    /// <returns>List of dates</returns>
    [HttpGet("patients/over-age-with-multiple-doctors")]
    public ActionResult<List<DateTime>> GetPatientsOverAgeWithMultipleDoctors([FromQuery] int age, [FromQuery] DateTime date)
    {
        try
        {
            var result = analyticsService.GetPatientsOverAgeWithMultipleDoctors(age, date);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving patients over age {Age} with multiple doctors as of {Date}", age, date);
            return BadRequest($"An error occurred while processing the request: {ex.Message}");
        }
    }

    /// <summary>
    /// Get appointments in the cabinet for the current month
    /// </summary>
    /// <param name="roomNumber">Cabinet number</param>
    /// <param name="currentDate">Current date</param>
    /// <returns>List of appointments</returns>
    [HttpGet("appointments/in-cabinet/{roomNumber}")]
    public ActionResult<List<AppointmentDto>> GetAppointmentsInCabinetForCurrentMonth(int roomNumber, [FromQuery] DateTime currentDate)
    {
        try
        {
            var result = analyticsService.GetAppointmentsInCabinetForCurrentMonth(roomNumber, currentDate);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving appointments in cabinet {RoomNumber} for month of {CurrentDate}", roomNumber, currentDate);
            return BadRequest($"An error occurred while processing the request: {ex.Message}");
        }
    }
}