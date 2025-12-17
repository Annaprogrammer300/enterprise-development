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
/// <param name="patientManager">Patient manager</param>
/// <param name="doctorManager">Doctor manager</param>
/// <param name="mapper">Mapping profile</param>
public class AppointmentService(IManager<Appointment, int> manager, IManager<Patient, int> patientManager,
    IManager<Doctor, int> doctorManager, IMapper mapper) : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    /// <summary>
    /// Creates a new appointment entity from the provided DTO
    /// </summary>
    /// <param name="dto">Data transfer object containing appointment creation details</param>
    /// <returns>AppointmentDto representing the created appointment</returns>
    public AppointmentDto Create(AppointmentCreateUpdateDto dto)
    {
        var newAppointment = mapper.Map<Appointment>(dto);
        var patient = patientManager.Read(dto.PatientId);
        var doctor = doctorManager.Read(dto.DoctorId);
        if (patient == null)
            throw new ArgumentException($"Patient with id {dto.PatientId} not found");
        if (doctor == null)
            throw new ArgumentException($"Doctor with id {dto.DoctorId} not found");
        newAppointment.Patient = patient;
        newAppointment.Doctor = doctor;
        var createdAppointment = manager.Create(newAppointment);
        return mapper.Map<AppointmentDto>(createdAppointment);
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
        var existingAppointment = manager.Read(dtoId) ?? throw new KeyNotFoundException("Entity not found");
        var patient = patientManager.Read(dto.PatientId);
        var doctor = doctorManager.Read(dto.DoctorId);

        if (patient == null)
            throw new KeyNotFoundException($"Patient with id {dto.PatientId} not found");
        if (doctor == null)
            throw new KeyNotFoundException($"Doctor with id {dto.DoctorId} not found");

        mapper.Map(dto, existingAppointment);
        existingAppointment.Patient = patient;
        existingAppointment.Doctor = doctor;
        manager.Update(existingAppointment);
        return mapper.Map<AppointmentDto>(existingAppointment);
    }
}