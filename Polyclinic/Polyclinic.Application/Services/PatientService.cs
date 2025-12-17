using AutoMapper;
using Microsoft.Extensions.Logging;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;

namespace Polyclinic.Application.Services;

/// <summary>
/// Service for CRUD operations on patients
/// </summary>
public class PatientService(IManager<Patient, int> manager, IMapper mapper, ILogger<PatientService> logger) : IApplicationService<PatientDto, PatientCreateUpdateDto, int>
{
    /// <summary>
    /// Creates a new patient entity from the provided DTO
    /// </summary>
    public PatientDto Create(PatientCreateUpdateDto dto)
    {
        var newPatient = mapper.Map<Patient>(dto);
        var createdPatient = manager.Create(newPatient);
        var result = mapper.Map<PatientDto>(createdPatient);

        logger.LogInformation("The patient was created successfully. ID: {patientId}", result.Id);

        return result;
    }

    /// <summary>
    /// Deletes a patient entity by its unique identifier
    /// </summary>
    public void Delete(int dtoId)
    {
        manager.Delete(dtoId);
    }

    /// <summary>
    /// Retrieves a patient entity by its unique identifier
    /// </summary>
    public PatientDto Get(int dtoId)
    {
        var entity = manager.Read(dtoId);
        return entity == null ? throw new KeyNotFoundException("Entity not found") : mapper.Map<PatientDto>(entity);
    }

    /// <summary>
    /// Retrieves all patient entities
    /// </summary>
    public List<PatientDto> GetAll()
    {
        var patients = manager.ReadAll();
        return mapper.Map<List<PatientDto>>(patients);
    }

    /// <summary>
    /// Updates an existing patient entity with the provided DTO data
    /// </summary>
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