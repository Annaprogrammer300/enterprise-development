using Polyclinic;

namespace Polyclinic.Application.Interfaces;

/// <summary>
/// Интерфейс для доменной службы врачей
/// </summary>
public interface IDoctorManager
{
    /// <summary>
    /// Получает врачей с опытом работы не менее указанного
    /// </summary>
    /// <param name="minExperience">Минимальный опыт работы в годах</param>
    /// <returns>Список врачей</returns>
    public Task<List<Doctor>> GetDoctorsWithMinExperienceAsync(int minExperience);

    /// <summary>
    /// Получает топ 5 врачей по количеству приемов
    /// </summary>
    /// <returns>Список кортежей вида (врач, количество приемов)</returns>
    public Task<List<KeyValuePair<Doctor, int>>> GetTop5DoctorsByAppointmentsCountAsync();
}