using Polyclinic.Domain;

namespace Polyclinic.Infrastructure.InMemory;

/// <summary>
/// Имплементация менеджера для врачей
/// </summary>
public class DoctorInMemoryManager : IManager<Doctor, int>
{
    private readonly List<Doctor> _doctors;

    /// <inheritdoc/>
    public DoctorInMemoryManager()
    {
        _doctors = TestDataSeeder.GetDoctors();
    }

    /// <inheritdoc/>
    public void Create(Doctor entity)
    {
        _doctors.Add(entity);
    }

    /// <inheritdoc/>
    public Doctor Read(int entityId)
    {
        return _doctors.FirstOrDefault(d => d.Id == entityId);
    }

    /// <inheritdoc/>
    public List<Doctor> ReadAll()
    {
        return _doctors;
    }

    /// <inheritdoc/>
    public void Update(Doctor entity)
    {
        var existingDoctor = Read(entity.Id);
        if (existingDoctor != null)
        {
            _doctors.Remove(existingDoctor);
            _doctors.Add(entity);
        }
    }

    /// <inheritdoc/>
    public void Delete(int entityId)
    {
        var doctor = Read(entityId);
        if (doctor != null)
            _doctors.Remove(doctor);
    }
}
