using Polyclinic;

namespace Polyclinic.Application.Interfaces;

/// <summary>
/// Интерфейс для доменной службы записей на прием
/// </summary>
public interface IAppointmentManager
{
    /// <summary>
    /// Получает повторные приемы за указанный период
    /// </summary>
    /// <param name="startDate">Начальная дата периода</param>
    /// <param name="endDate">Конечная дата периода</param>
    /// <returns>Список приемов</returns>
    public Task<List<Appointment>> GetRepeatedAppointmentsAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Получает приемы в указанном кабинете за текущий месяц
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="currentDate">Текущая дата</param>
    /// <returns>Список приемов</returns>
    public Task<List<Appointment>> GetAppointmentsInRoomForCurrentMonthAsync(int roomNumber, DateTime currentDate);
}