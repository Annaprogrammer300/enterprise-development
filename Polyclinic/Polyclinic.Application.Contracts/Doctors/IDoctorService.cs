using Polyclinic.Enum;

namespace Polyclinic.Application.Contracts.Doctors;

/// <summary>
/// Наследник аппликейшен службы для врачей
/// </summary>
public interface IDoctorService : IApplicationService<DoctorDto, DoctorCreateUpdateDto, int>
{
    /// <summary>
    /// Получает врачей по специализации
    /// </summary>
    /// <param name="specialization">Специализация</param>
    /// <returns>Список DTO врачей</returns>
    public Task<List<DoctorDto>> GetBySpecializationAsync(Specialization? specialization);

    /// <summary>
    /// Получает врачей с минимальным опытом
    /// </summary>
    /// <param name="minExperience">Минимальный опыт</param>
    /// <returns>Список DTO врачей</returns>
    public Task<List<DoctorDto>> GetWithMinExperienceAsync(int? minExperience);
}