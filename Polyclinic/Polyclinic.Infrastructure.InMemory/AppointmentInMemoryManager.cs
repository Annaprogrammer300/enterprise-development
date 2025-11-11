using Polyclinic.Domain;

namespace Polyclinic.Infrastructure.InMemory;

/// <summary>
/// Имплементация менеджера для записей на прием
/// </summary>
public class AppointmentInMemoryManager : IManager<Appointment, int>
{
    private readonly List<Appointment> _appointments;
    private readonly List<Patient> _patients;
    private readonly List<Doctor> _doctors;

    /// <inheritdoc/>
    public AppointmentInMemoryManager()
    {
        _patients = TestDataSeeder.GetPatients();
        _doctors = TestDataSeeder.GetDoctors();
        _appointments = TestDataSeeder.GetAppointments(_patients, _doctors);
    }

    /// <inheritdoc/>
    public void Create(Appointment entity)
    {
        _appointments.Add(entity);
    }

    /// <inheritdoc/>
    public Appointment Read(int entityId)
    {
        return _appointments.FirstOrDefault(a => a.Id == entityId);
    }

    /// <inheritdoc/>
    public List<Appointment> ReadAll()
    {
        return _appointments;
    }

    /// <inheritdoc/>
    public void Update(Appointment entity)
    {
        var existingAppointment = Read(entity.Id);
        if (existingAppointment != null)
        {
            _appointments.Remove(existingAppointment);
            _appointments.Add(entity);
        }
    }

    /// <inheritdoc/>
    public void Delete(int entityId)
    {
        var appointment = Read(entityId);
        if (appointment != null)
            _appointments.Remove(appointment);
    }
}
