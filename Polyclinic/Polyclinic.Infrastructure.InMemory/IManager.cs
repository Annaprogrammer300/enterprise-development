namespace Polyclinic.Infrastructure.InMemory;

/// <summary>
/// Интерфейс менеджера для CRUD операций
/// </summary>
/// <typeparam name="TEntity">Тип сущности, доступ к коллекции которых абстрагируем</typeparam>
/// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
public interface IManager<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Создание новой сущности
    /// </summary>
    /// <param name="entity">Новая сущность</param>
    public void Create(TEntity entity);

    /// <summary>
    /// Получение сущности по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <returns>Сущность</returns>
    public TEntity Read(TKey entityId);

    /// <summary>
    /// Получение всего списка сущностей
    /// </summary>
    /// <returns></returns>
    public List<TEntity> ReadAll();

    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="entity">Сущность</param>
    public void Update(TEntity entity);

    /// <summary>
    /// Удаление сущности по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    public void Delete(TKey entityId);
}