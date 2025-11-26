using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;

namespace Polyclinic.Infrastructure.InMemory;

/// <summary>
/// In-memory implementation of the patient manager for CRUD operations
/// </summary>
public class PatientInMemoryManager : IManager<Patient, int>
{
    private readonly List<Patient> _patients = DataSeeder.GetPatients();

    /// <summary>
    /// Adds a new Patient entity to the in-memory collection.
    /// </summary>
    /// <param name="entity">The Patient entity to add.</param>
    public void Create(Patient entity) => _patients.Add(entity);

    /// <summary>
    /// Retrieves a Patient entity by its unique identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the Patient.</param>
    /// <returns>The Patient entity if found; otherwise, null.</returns>
    public Patient? Read(int entityId) => _patients.FirstOrDefault(p => p.Id == entityId);

    /// <summary>
    /// Retrieves all Patient entities from the in-memory collection.
    /// </summary>
    /// <returns>A list of all Patient entities.</returns>
    public List<Patient> ReadAll() => _patients;

    /// <summary>
    /// Updates an existing Patient entity in the in-memory collection.
    /// </summary>
    /// <param name="entity">The Patient entity with updated information.</param>
    public void Update(Patient entity)
    {
        var index = _patients.FindIndex(p => p.Id == entity.Id);
        if (index != -1) _patients[index] = entity;
    }

    /// <summary>
    /// Removes a Patient entity from the in-memory collection by its identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the Patient to remove.</param>
    public void Delete(int entityId)
    {
        var item = Read(entityId);
        if (item != null) _patients.Remove(item);
    }

    /// <summary>
    /// Checks whether a Patient entity with the specified identifier exists in the collection.
    /// </summary>
    /// <param name="entityId">The unique identifier to check.</param>
    /// <returns>true if a Patient with the specified ID exists; otherwise, false.</returns>
    public bool Exists(int entityId) => _patients.Any(p => p.Id == entityId);
}