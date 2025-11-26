using Polyclinic.Domain;

namespace Polyclinic.Infrastructure.InMemory;

/// <summary>
/// In-memory implementation of the manager for doctors providing CRUD operations
/// </summary>
public class DoctorInMemoryManager : IManager<Doctor, int>
{
    private readonly List<Doctor> _doctors;

    /// <summary>
    /// Initializes a new instance of the DoctorInMemoryManager class with pre-seeded test data
    /// </summary>
    public DoctorInMemoryManager()
    {
        _doctors = DataSeeder.GetDoctors();
    }

    /// <summary>
    /// Creates a new doctor entity and adds it to the in-memory collection
    /// </summary>
    /// <param name="entity">The doctor entity to create</param>
    public void Create(Doctor entity)
    {
        _doctors.Add(entity);
    }

    /// <summary>
    /// Retrieves a doctor entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the doctor</param>
    /// <returns>The doctor entity if found, otherwise null</returns>
    public Doctor Read(int entityId)
    {
        return _doctors.FirstOrDefault(d => d.Id == entityId);
    }

    /// <summary>
    /// Retrieves all doctor entities from the in-memory collection
    /// </summary>
    /// <returns>A list of all doctor entities</returns>
    public List<Doctor> ReadAll()
    {
        return _doctors;
    }

    /// <summary>
    /// Updates an existing doctor entity in the in-memory collection
    /// </summary>
    /// <param name="entity">The doctor entity with updated data</param>
    public void Update(Doctor entity)
    {
        var existingDoctor = Read(entity.Id);
        if (existingDoctor != null)
        {
            _doctors.Remove(existingDoctor);
            _doctors.Add(entity);
        }
    }

    /// <summary>
    /// Deletes a doctor entity from the in-memory collection by its identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the doctor to delete</param>
    public void Delete(int entityId)
    {
        var doctor = Read(entityId);
        if (doctor != null)
            _doctors.Remove(doctor);
    }
}