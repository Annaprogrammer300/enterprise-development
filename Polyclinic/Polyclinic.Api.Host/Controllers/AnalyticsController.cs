using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;

namespace Polyclinic.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Get a list of doctors with at least the specified number of years of experience
    /// </summary>
    /// <param name="minExperience">Minimum work experience in years</param>
    /// <returns>List of doctor IDs</returns>
    [HttpGet("doctors/experience/{minExperience}")]
    public ActionResult<List<int>> GetDoctorsWithExperienceAtLeast(int minExperience)
    {
        var result = analyticsService.GetDoctorsWithExperienceAtLeast(minExperience);
        return Ok(result);
    }

    /// <summary>
    /// Get a list of patients by doctor ID
    /// </summary>
    /// <param name="doctorId">Doctor's ID</param>
    /// <returns>List of patients</returns>
    [HttpGet("patients/by-doctor/{doctorId}")]
    public ActionResult<List<string>> GetPatientsByDoctor(int doctorId)
    {
        var result = analyticsService.GetPatientsByDoctor(doctorId);
        return Ok(result);
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
        var result = analyticsService.CountRepeatedAppointmentsLastMonth(lastMonth, today);
        return Ok(result);
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
        var result = analyticsService.GetPatientsOverAgeWithMultipleDoctors(age, date);
        return Ok(result);
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
        var result = analyticsService.GetAppointmentsInCabinetForCurrentMonth(roomNumber, currentDate);
        return Ok(result);
    }
}