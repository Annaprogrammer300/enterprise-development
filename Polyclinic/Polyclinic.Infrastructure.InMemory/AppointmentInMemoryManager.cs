using Polyclinic.Domain;

namespace Polyclinic.Infrastructure.InMemory;

/// <summary>
/// In-memory implementation of the appointment manager providing CRUD operations for appointments
/// </summary>
public class AppointmentInMemoryManager : IManager<Appointment, int>
{
    private readonly List<Appointment> _appointments;
    private readonly List<Patient> _patients;
    private readonly List<Doctor> _doctors;

    /// <summary>
    /// Initializes a new instance of the AppointmentInMemoryManager class with pre-seeded test data including related patients and doctors
    /// </summary>
    public AppointmentInMemoryManager()
    {
        _patients = TestDataSeeder.GetPatients();
        _doctors = TestDataSeeder.GetDoctors();
        _appointments = TestDataSeeder.GetAppointments(_patients, _doctors);
    }

    /// <summary>
    /// Creates a new appointment entity and adds it to the in-memory collection
    /// </summary>
    /// <param name="entity">The appointment entity to create</param>
    public void Create(Appointment entity)
    {
        _appointments.Add(entity);
    }

    /// <summary>
    /// Retrieves an appointment entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the appointment</param>
    /// <returns>The appointment entity if found, otherwise null</returns>
    public Appointment Read(int entityId)
    {
        return _appointments.FirstOrDefault(a => a.Id == entityId);
    }

    /// <summary>
    /// Retrieves all appointment entities from the in-memory collection
    /// </summary>
    /// <returns>A list of all appointment entities</returns>
    public List<Appointment> ReadAll()
    {
        return _appointments;
    }

    /// <summary>
    /// Updates an existing appointment entity in the in-memory collection
    /// </summary>
    /// <param name="entity">The appointment entity with updated data</param>
    public void Update(Appointment entity)
    {
        var existingAppointment = Read(entity.Id);
        if (existingAppointment != null)
        {
            _appointments.Remove(existingAppointment);
            _appointments.Add(entity);
        }
    }

    /// <summary>
    /// Deletes an appointment entity from the in-memory collection by its identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the appointment to delete</param>
    public void Delete(int entityId)
    {
        var appointment = Read(entityId);
        if (appointment != null)
            _appointments.Remove(appointment);
    }
}