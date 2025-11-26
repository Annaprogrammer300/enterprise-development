using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;

namespace Polyclinic.Application.Services;

/// <summary>
/// Service for CRUD operations on appointment appointments
/// </summary>
/// <param name="manager">Record manager</param>
/// <param name="mapper">Mapping profile</param>
public class AppointmentService(IManager<Appointment, int> manager, IMapper mapper) : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    /// <summary>
    /// Creates a new appointment entity from the provided DTO
    /// </summary>
    /// <param name="dto">Data transfer object containing appointment creation details</param>
    /// <returns>AppointmentDto representing the created appointment</returns>
    public AppointmentDto Create(AppointmentCreateUpdateDto dto)
    {
        var newAppointment = mapper.Map<Appointment>(dto);
        var lastId = manager.ReadAll().Count > 0 ? manager.ReadAll().Max(a => a.Id) : 0;
        newAppointment.Id = lastId + 1;
        manager.Create(newAppointment);
        return mapper.Map<AppointmentDto>(newAppointment);
    }

    /// <summary>
    /// Deletes an appointment entity by its unique identifier
    /// </summary>
    /// <param name="dtoId">The unique identifier of the appointment to delete</param>
    public void Delete(int dtoId)
    {
        manager.Delete(dtoId);
    }

    /// <summary>
    /// Retrieves an appointment entity by its unique identifier
    /// </summary>
    /// <param name="dtoId">The unique identifier of the appointment</param>
    /// <returns>AppointmentDto representing the found appointment</returns>
    public AppointmentDto Get(int dtoId)
    {
        var entity = manager.Read(dtoId);
        return entity == null ? throw new KeyNotFoundException("Entity not found") : mapper.Map<AppointmentDto>(entity);
    }

    /// <summary>
    /// Retrieves all appointment entities
    /// </summary>
    /// <returns>List of AppointmentDto representing all appointments</returns>
    public List<AppointmentDto> GetAll()
    {
        var appointments = manager.ReadAll();
        return mapper.Map<List<AppointmentDto>>(appointments);
    }

    /// <summary>
    /// Updates an existing appointment entity with the provided DTO data
    /// </summary>
    /// <param name="dtoId">The unique identifier of the appointment to update</param>
    /// <param name="dto">Data transfer object containing updated appointment details</param>
    /// <returns>AppointmentDto representing the updated appointment</returns>
    /// <exception cref="ArgumentException">Thrown when appointment with specified ID is not found</exception>
    public AppointmentDto Update(int dtoId, AppointmentCreateUpdateDto dto)
    {
        if (!manager.Exists(dtoId))
        {
            throw new KeyNotFoundException("Entity not found");
        }

        var entity = mapper.Map<Appointment>(dto);
        entity.Id = dtoId;
        manager.Update(entity);
        return mapper.Map<AppointmentDto>(entity);
    }
}