using Polyclinic.Application.Contracts.Appointments;

namespace Polyclinic.Application.Contracts;

/// <summary>
/// Интерфейс службы аналитики
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получение врачей с опытом работы не менее указанного
    /// </summary>
    /// <param name="minExperience">Минимальный опыт работы</param>
    /// <returns></returns>
    public List<int> GetDoctorsWithExperienceAtLeast(int minExperience);

    /// <summary>
    /// Получение пациентов по указанному врачу
    /// </summary>
    /// <param name="doctorId">Идентификатор врача</param>
    /// <returns></returns>
    public List<string> GetPatientsByDoctor(int doctorId);

    /// <summary>
    /// Подсчет повторных приемов за последний месяц
    /// </summary>
    /// <param name="lastMonth">Дата начала последнего месяца</param>
    /// <param name="today">Текущая дата</param>
    /// <returns></returns>
    public int CountRepeatedAppointmentsLastMonth(DateTime lastMonth, DateTime today);

    /// <summary>
    /// Получение пациентов старше указанного возраста, которые посещали нескольких врачей
    /// </summary>
    /// <param name="age">Минимальный возраст</param>
    /// <param name="date">Текущая дата для расчета возраста</param>
    /// <returns></returns>
    public List<DateTime> GetPatientsOverAgeWithMultipleDoctors(int age, DateTime date);

    /// <summary>
    /// Получение приемов в указанном кабинете за текущий месяц
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="currentDate">Текущая дата</param>
    /// <returns></returns>
    public List<AppointmentDto> GetAppointmentsInCabinetForCurrentMonth(int roomNumber, DateTime currentDate);
}