using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;

namespace Polyclinic.Infrastructure.InMemory;

/// <summary>
/// In-memory implementation of the appointment manager providing CRUD operations for appointments
/// </summary>
public class AppointmentInMemoryManager : IManager<Appointment, int>
{
    private readonly List<Appointment> _appointments = DataSeeder.GetAppointments();

    /// <summary>
    /// Adds a new Appointment entity to the in-memory collection.
    /// </summary>
    /// <param name="entity">The Appointment entity to add.</param>
    public Appointment Create(Appointment entity)
    {
        _appointments.Add(entity);
        return entity;
    }

    /// <summary>
    /// Retrieves an Appointment entity by its unique identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the Appointment.</param>
    /// <returns>The Appointment entity if found; otherwise, null.</returns>
    public Appointment? Read(int entityId) => _appointments.FirstOrDefault(a => a.Id == entityId);

    /// <summary>
    /// Retrieves all Appointment entities from the in-memory collection.
    /// </summary>
    /// <returns>A list of all Appointment entities.</returns>
    public List<Appointment> ReadAll() => _appointments;

    /// <summary>
    /// Updates an existing Appointment entity in the in-memory collection.
    /// </summary>
    /// <param name="entity">The Appointment entity with updated information.</param>
    public void Update(Appointment entity)
    {
        var index = _appointments.FindIndex(a => a.Id == entity.Id);
        if (index != -1) _appointments[index] = entity;
    }

    /// <summary>
    /// Removes an Appointment entity from the in-memory collection by its identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the Appointment to remove.</param>
    public void Delete(int entityId)
    {
        var item = Read(entityId);
        if (item != null) _appointments.Remove(item);
    }

    /// <summary>
    /// Checks whether an Appointment entity with the specified identifier exists in the collection.
    /// </summary>
    /// <param name="entityId">The unique identifier to check.</param>
    /// <returns>true if an Appointment with the specified ID exists; otherwise, false.</returns>
    public bool Exists(int entityId) => _appointments.Any(a => a.Id == entityId);
}