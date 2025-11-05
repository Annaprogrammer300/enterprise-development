using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Appointments;

namespace Polyclinic.Application.Contracts;

/// <summary>
/// Базовый интерфейс аппликейшен службы
/// </summary>
/// <typeparam name="TDto">Тип DTO</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания/обновления</typeparam>
/// <typeparam name="TKey">Тип ключа</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
{
    /// <summary>
    /// Получает все сущности
    /// </summary>
    /// <returns>Коллекция DTO</returns>
    public Task<List<TDto>> GetAsync();

    /// <summary>
    /// Получает сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>DTO сущности</returns>
    public Task<TDto?> GetAsync(TKey id);

    /// <summary>
    /// Создает новую сущность
    /// </summary>
    /// <param name="dto">DTO для создания</param>
    /// <returns>Созданная DTO</returns>
    public Task<TDto> CreateAsync(TCreateUpdateDto dto);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="dto">DTO для обновления</param>
    /// <returns>Обновленная DTO</returns>
    public Task<TDto> UpdateAsync(TKey id, TCreateUpdateDto dto);

    /// <summary>
    /// Удаляет сущность
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    public Task DeleteAsync(TKey id);
}