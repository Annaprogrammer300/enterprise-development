using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace Polyclinic.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список докторов с опытом работы не менее указанного количества лет
    /// </summary>
    /// <param name="minExperience">Минимальный опыт работы в годах</param>
    /// <returns>Список идентификаторов докторов</returns>
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
    /// Получить список пациентов по идентификатору доктора
    /// </summary>
    /// <param name="doctorId">Идентификатор доктора</param>
    /// <returns>Список пациентов</returns>
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
    /// Получить количество повторных назначений за последний месяц
    /// </summary>
    /// <param name="lastMonth">Начало периода (последний месяц)</param>
    /// <param name="today">Конец периода (сегодня)</param>
    /// <returns>Количество повторных назначений</returns>
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
    /// Получить список дат для пациентов старше указанного возраста с несколькими докторами
    /// </summary>
    /// <param name="age">Минимальный возраст</param>
    /// <param name="date">Дата для расчета возраста</param>
    /// <returns>Список дат</returns>
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
    /// Получить назначения в кабинете за текущий месяц
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="currentDate">Текущая дата</param>
    /// <returns>Список назначений</returns>
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