using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;

namespace Polyclinic.Infrastructure.InMemory;

/// <summary>
/// In-memory implementation of the manager for doctors providing CRUD operations
/// </summary>
public class DoctorInMemoryManager : IManager<Doctor, int>
{
    private readonly List<Doctor> _doctors = DataSeeder.GetDoctors();

    /// <summary>
    /// Adds a new Doctor entity to the in-memory collection.
    /// </summary>
    /// <param name="entity">The Doctor entity to add.</param>
    public void Create(Doctor entity) => _doctors.Add(entity);

    /// <summary>
    /// Retrieves a Doctor entity by its unique identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the Doctor.</param>
    /// <returns>The Doctor entity if found; otherwise, null.</returns>
    public Doctor? Read(int entityId) => _doctors.FirstOrDefault(d => d.Id == entityId);

    /// <summary>
    /// Retrieves all Doctor entities from the in-memory collection.
    /// </summary>
    /// <returns>A list of all Doctor entities.</returns>
    public List<Doctor> ReadAll() => _doctors;

    /// <summary>
    /// Updates an existing Doctor entity in the in-memory collection.
    /// </summary>
    /// <param name="entity">The Doctor entity with updated information.</param>
    public void Update(Doctor entity)
    {
        var index = _doctors.FindIndex(d => d.Id == entity.Id);
        if (index != -1) _doctors[index] = entity;
    }

    /// <summary>
    /// Removes a Doctor entity from the in-memory collection by its identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the Doctor to remove.</param>
    public void Delete(int entityId)
    {
        var item = Read(entityId);
        if (item != null) _doctors.Remove(item);
    }

    /// <summary>
    /// Checks whether a Doctor entity with the specified identifier exists in the collection.
    /// </summary>
    /// <param name="entityId">The unique identifier to check.</param>
    /// <returns>true if a Doctor with the specified ID exists; otherwise, false.</returns>
    public bool Exists(int entityId) => _doctors.Any(d => d.Id == entityId);
}