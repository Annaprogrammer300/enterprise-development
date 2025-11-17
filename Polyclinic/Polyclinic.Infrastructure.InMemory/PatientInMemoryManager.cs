using Polyclinic.Domain;

namespace Polyclinic.Infrastructure.InMemory;

/// <summary>
/// In-memory implementation of the patient manager for CRUD operations
/// </summary>
public class PatientInMemoryManager : IManager<Patient, int>
{
    private readonly List<Patient> _patients;

    /// <summary>
    /// Initializes a new instance of the PatientInMemoryManager class with test data
    /// </summary>
    public PatientInMemoryManager()
    {
        _patients = TestDataSeeder.GetPatients();
    }

    /// <summary>
    /// Creates a new patient entity and adds it to the in-memory collection
    /// </summary>
    /// <param name="entity">The patient entity to create</param>
    public void Create(Patient entity)
    {
        _patients.Add(entity);
    }

    /// <summary>
    /// Retrieves a patient entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the patient</param>
    /// <returns>The patient entity if found, otherwise null</returns>
    public Patient Read(int entityId)
    {
        return _patients.FirstOrDefault(p => p.Id == entityId);
    }

    /// <summary>
    /// Retrieves all patient entities from the in-memory collection
    /// </summary>
    /// <returns>A list of all patient entities</returns>
    public List<Patient> ReadAll()
    {
        return _patients;
    }

    /// <summary>
    /// Updates an existing patient entity in the in-memory collection
    /// </summary>
    /// <param name="entity">The patient entity with updated data</param>
    public void Update(Patient entity)
    {
        var existingPatient = Read(entity.Id);
        if (existingPatient != null)
        {
            _patients.Remove(existingPatient);
            _patients.Add(entity);
        }
    }

    /// <summary>
    /// Deletes a patient entity from the in-memory collection by its identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the patient to delete</param>
    public void Delete(int entityId)
    {
        var patient = Read(entityId);
        if (patient != null)
            _patients.Remove(patient);
    }
}