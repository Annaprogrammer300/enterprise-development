namespace Polyclinic.Domain.Abstractions;

/// <summary>
/// The manager's interface for CRUD operations
/// </summary>
/// <typeparam name="TEntity">The type of entity whose collection is being abstracted</typeparam>
/// <typeparam name="TKey">The type of the entity ID</typeparam>
public interface IManager<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Creating a new entity
    /// </summary>
    /// <param name="entity">New entity</param>
    public void Create(TEntity entity);

    /// <summary>
    /// Getting an entity by ID
    /// </summary>
    /// <param name="entityId">Entity ID</param>
    /// <returns>Entity if found; otherwise, null</returns>
    public TEntity? Read(TKey entityId);

    /// <summary>
    /// Getting the entire list of entities
    /// </summary>
    /// <returns></returns>
    public List<TEntity> ReadAll();

    /// <summary>
    /// Updating the entity
    /// </summary>
    /// <param name="entity">The entity</param>
    public void Update(TEntity entity);

    /// <summary>
    /// Deleting an entity by ID
    /// </summary>
    /// <param name="entityId">Entity ID</param>
    public void Delete(TKey entityId);

    /// <summary>
    /// Checks if an entity exists by its ID
    /// </summary>
    /// <param name="entityId">Entity ID</param>
    /// <returns>True if exists, otherwise false</returns>
    public bool Exists(TKey entityId);
}