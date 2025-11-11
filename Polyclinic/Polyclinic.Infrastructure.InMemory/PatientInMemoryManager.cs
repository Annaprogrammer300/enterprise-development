using Polyclinic.Domain;

namespace Polyclinic.Infrastructure.InMemory;

/// <summary>
/// Имплементация менеджера для пациентов
/// </summary>
public class PatientInMemoryManager : IManager<Patient, int>
{
    private readonly List<Patient> _patients;

    /// <inheritdoc/>
    public PatientInMemoryManager()
    {
        _patients = TestDataSeeder.GetPatients();
    }

    /// <inheritdoc/>
    public void Create(Patient entity)
    {
        _patients.Add(entity);
    }

    /// <inheritdoc/>
    public Patient Read(int entityId)
    {
        return _patients.FirstOrDefault(p => p.Id == entityId);
    }

    /// <inheritdoc/>
    public List<Patient> ReadAll()
    {
        return _patients;
    }

    /// <inheritdoc/>
    public void Update(Patient entity)
    {
        var existingPatient = Read(entity.Id);
        if (existingPatient != null)
        {
            _patients.Remove(existingPatient);
            _patients.Add(entity);
        }
    }

    /// <inheritdoc/>
    public void Delete(int entityId)
    {
        var patient = Read(entityId);
        if (patient != null)
            _patients.Remove(patient);
    }
}