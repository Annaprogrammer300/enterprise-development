using Polyclinic;

namespace Polyclinic.Application.Interfaces;

/// <summary>
/// Интерфейс для доменной службы пациентов
/// </summary>
public interface IPatientManager
{
    /// <summary>
    /// Получает пациентов старше указанного возраста
    /// </summary>
    /// <param name="age">Минимальный возраст</param>
    /// <param name="currentDate">Текущая дата для расчета возраста</param>
    /// <returns>Список пациентов</returns>
    public Task<List<Patient>> GetPatientsOlderThanAsync(int age, DateTime currentDate);

    /// <summary>
    /// Получает пациентов с указанной группой крови и резус-фактором
    /// </summary>
    /// <param name="bloodGroup">Группа крови</param>
    /// <param name="rhesusFactor">Резус-фактор</param>
    /// <returns>Список пациентов</returns>
    public Task<List<Patient>> GetPatientsByBloodGroupAsync(string bloodGroup, string rhesusFactor);
}