namespace Polyclinic.Application.Contracts;

/// <summary>
/// Application service interface for CRUD operations
/// </summary>
/// <typeparam name="TDto">DTO for Get requests</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO for Post/Put requests</typeparam>
/// <typeparam name="TKey">ID type DTO</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Creating a DTO
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns></returns>
    public TDto Create(TCreateUpdateDto dto);

    /// <summary>
    /// Getting a DTO by ID
    /// </summary>
    /// <param name="dtoId">DTO ID</param>
    /// <returns></returns>
    public TDto Get(TKey dtoId);

    /// <summary>
    /// Getting the entire list of DTOs
    /// </summary>
    /// <returns></returns>
    public List<TDto> GetAll();

    /// <summary>
    /// DTO update
    /// </summary>
    /// <param name="dtoId">DTO ID</param>
    /// <param name="dto">DTO</param>
    /// <returns></returns>
    public TDto Update(TKey dtoId, TCreateUpdateDto dto);

    /// <summary>
    /// Removing DTO
    /// </summary>
    /// <param name="dtoId">DTO ID</param>
    public void Delete(TKey dtoId);
}