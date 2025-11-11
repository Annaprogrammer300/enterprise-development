using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Infrastructure.InMemory;

namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для CRUD-операций над пациентами
/// </summary>
/// <param name="manager">Менеджер пациентов</param>
/// <param name="mapper">Профиль маппинга</param>
public class PatientService(IManager<Patient, int> manager, IMapper mapper) : IApplicationService<PatientDto, PatientCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public PatientDto Create(PatientCreateUpdateDto dto)
    {
        var newPatient = mapper.Map<Patient>(dto);
        var lastId = manager.ReadAll().Count > 0 ? manager.ReadAll().Max(p => p.Id) : 0;
        newPatient.Id = lastId + 1;
        manager.Create(newPatient);
        return mapper.Map<PatientDto>(newPatient);
    }

    /// <inheritdoc/>
    public void Delete(int dtoId)
    {
        manager.Delete(dtoId);
    }

    /// <inheritdoc/>
    public PatientDto Get(int dtoId)
    {
        var patient = manager.Read(dtoId);
        return mapper.Map<PatientDto>(patient);
    }

    /// <inheritdoc/>
    public List<PatientDto> GetAll()
    {
        var patients = manager.ReadAll();
        return mapper.Map<List<PatientDto>>(patients);
    }

    /// <inheritdoc/>
    public PatientDto Update(int dtoId, PatientCreateUpdateDto dto)
    {
        _ = manager.Read(dtoId) ?? throw new ArgumentException("Patient not found");
        var updatedPatient = mapper.Map<Patient>(dto);
        updatedPatient.Id = dtoId;
        manager.Update(updatedPatient);
        return mapper.Map<PatientDto>(updatedPatient);
    }
}