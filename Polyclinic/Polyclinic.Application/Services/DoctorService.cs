using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Infrastructure.InMemory;

namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для CRUD-операций над врачами
/// </summary>
/// <param name="manager">Менеджер врачей</param>
/// <param name="mapper">Профиль маппинга</param>
public class DoctorService(IManager<Doctor, int> manager, IMapper mapper) : IApplicationService<DoctorDto, DoctorCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public DoctorDto Create(DoctorCreateUpdateDto dto)
    {
        var newDoctor = mapper.Map<Doctor>(dto);
        var lastId = manager.ReadAll().Count > 0 ? manager.ReadAll().Max(d => d.Id) : 0;
        newDoctor.Id = lastId + 1;
        manager.Create(newDoctor);
        return mapper.Map<DoctorDto>(newDoctor);
    }

    /// <inheritdoc/>
    public void Delete(int dtoId)
    {
        manager.Delete(dtoId);
    }

    /// <inheritdoc/>
    public DoctorDto Get(int dtoId)
    {
        var doctor = manager.Read(dtoId);
        return mapper.Map<DoctorDto>(doctor);
    }

    /// <inheritdoc/>
    public List<DoctorDto> GetAll()
    {
        var doctors = manager.ReadAll();
        return mapper.Map<List<DoctorDto>>(doctors);
    }

    /// <inheritdoc/>
    public DoctorDto Update(int dtoId, DoctorCreateUpdateDto dto)
    {
        _ = manager.Read(dtoId) ?? throw new ArgumentException("Doctor not found");
        var updatedDoctor = mapper.Map<Doctor>(dto);
        updatedDoctor.Id = dtoId;
        manager.Update(updatedDoctor);
        return mapper.Map<DoctorDto>(updatedDoctor);
    }
}