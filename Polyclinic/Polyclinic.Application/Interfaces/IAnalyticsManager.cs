using Polyclinic;

namespace Polyclinic.Application.Interfaces;

/// <summary>
/// Интерфейс для доменной службы аналитики
/// </summary>
public interface IAnalyticsManager
{
    /// <summary>
    /// Получает пациентов старше 30 лет, которые посещали нескольких врачей
    /// </summary>
    /// <param name="currentDate">Текущая дата для расчета возраста</param>
    /// <returns>Список дат рождения пациентов</returns>
    public Task<List<DateTime>> GetPatientsOver30WithMultipleDoctorsAsync(DateTime currentDate);

    /// <summary>
    /// Получает статистику по специализациям врачей
    /// </summary>
    /// <returns>Список кортежей вида (специализация, количество врачей)</returns>
    public Task<List<KeyValuePair<string, int>>> GetDoctorsStatisticsBySpecializationAsync();

    /// <summary>
    /// Получает загрузку врачей (количество приемов) за указанный период
    /// </summary>
    /// <param name="startDate">Начальная дата</param>
    /// <param name="endDate">Конечная дата</param>
    /// <returns>Список кортежей вида (врач, количество приемов)</returns>
    public Task<List<KeyValuePair<Doctor, int>>> GetDoctorsWorkloadAsync(DateTime startDate, DateTime endDate);
}