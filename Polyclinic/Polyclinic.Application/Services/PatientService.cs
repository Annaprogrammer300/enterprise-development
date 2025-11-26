using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;

namespace Polyclinic.Application.Services;

/// <summary>
/// Service for CRUD operations on patients
/// </summary>
/// <param name="manager">Patient Manager</param>
/// <param name="mapper">Mapping profile</param>
public class PatientService(IManager<Patient, int> manager, IMapper mapper) : IApplicationService<PatientDto, PatientCreateUpdateDto, int>
{
    /// <summary>
    /// Creates a new patient entity from the provided DTO
    /// </summary>
    /// <param name="dto">Data transfer object containing patient creation details</param>
    /// <returns>PatientDto representing the created patient</returns>
    public PatientDto Create(PatientCreateUpdateDto dto)
    {
        var newPatient = mapper.Map<Patient>(dto);
        var lastId = manager.ReadAll().Count > 0 ? manager.ReadAll().Max(p => p.Id) : 0;
        newPatient.Id = lastId + 1;
        manager.Create(newPatient);
        return mapper.Map<PatientDto>(newPatient);
    }

    /// <summary>
    /// Deletes a patient entity by its unique identifier
    /// </summary>
    /// <param name="dtoId">The unique identifier of the patient to delete</param>
    public void Delete(int dtoId)
    {
        manager.Delete(dtoId);
    }

    /// <summary>
    /// Retrieves a patient entity by its unique identifier
    /// </summary>
    /// <param name="dtoId">The unique identifier of the patient</param>
    /// <returns>PatientDto representing the found patient</returns>
    public PatientDto Get(int dtoId)
    {
        var entity = manager.Read(dtoId);
        return entity == null ? throw new KeyNotFoundException("Entity not found") : mapper.Map<PatientDto>(entity);
    }

    /// <summary>
    /// Retrieves all patient entities
    /// </summary>
    /// <returns>List of PatientDto representing all patients</returns>
    public List<PatientDto> GetAll()
    {
        var patients = manager.ReadAll();
        return mapper.Map<List<PatientDto>>(patients);
    }

    /// <summary>
    /// Updates an existing patient entity with the provided DTO data
    /// </summary>
    /// <param name="dtoId">The unique identifier of the patient to update</param>
    /// <param name="dto">Data transfer object containing updated patient details</param>
    /// <returns>PatientDto representing the updated patient</returns>
    /// <exception cref="ArgumentException">Thrown when patient with specified ID is not found</exception>
    public PatientDto Update(int dtoId, PatientCreateUpdateDto dto)
    {
        if (!manager.Exists(dtoId))
        {
            throw new KeyNotFoundException("Entity not found");
        }

        var entity = mapper.Map<Patient>(dto);
        entity.Id = dtoId;
        manager.Update(entity);
        return mapper.Map<PatientDto>(entity);
    }
}