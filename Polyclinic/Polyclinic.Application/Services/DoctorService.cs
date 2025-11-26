using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;

namespace Polyclinic.Application.Services;

/// <summary>
/// Service for CRUD operations on doctors
/// </summary>
/// <param name="manager">Doctors' manager</param>
/// <param name="mapper">Mapping profile</param>
public class DoctorService(IManager<Doctor, int> manager, IMapper mapper) : IApplicationService<DoctorDto, DoctorCreateUpdateDto, int>
{
    /// <summary>
    /// Creates a new doctor entity from the provided DTO
    /// </summary>
    /// <param name="dto">Data transfer object containing doctor creation details</param>
    /// <returns>DoctorDto representing the created doctor</returns>
    public DoctorDto Create(DoctorCreateUpdateDto dto)
    {
        var newDoctor = mapper.Map<Doctor>(dto);
        var lastId = manager.ReadAll().Count > 0 ? manager.ReadAll().Max(d => d.Id) : 0;
        newDoctor.Id = lastId + 1;
        manager.Create(newDoctor);
        return mapper.Map<DoctorDto>(newDoctor);
    }

    /// <summary>
    /// Deletes a doctor entity by its unique identifier
    /// </summary>
    /// <param name="dtoId">The unique identifier of the doctor to delete</param>
    public void Delete(int dtoId)
    {
        manager.Delete(dtoId);
    }

    /// <summary>
    /// Retrieves a doctor entity by its unique identifier
    /// </summary>
    /// <param name="dtoId">The unique identifier of the doctor</param>
    /// <returns>DoctorDto representing the found doctor</returns>
    public DoctorDto Get(int dtoId)
    {
        var entity = manager.Read(dtoId);
        return entity == null ? throw new KeyNotFoundException("Entity not found") : mapper.Map<DoctorDto>(entity);
    }

    /// <summary>
    /// Retrieves all doctor entities
    /// </summary>
    /// <returns>List of DoctorDto representing all doctors</returns>
    public List<DoctorDto> GetAll()
    {
        var doctors = manager.ReadAll();
        return mapper.Map<List<DoctorDto>>(doctors);
    }

    /// <summary>
    /// Updates an existing doctor entity with the provided DTO data
    /// </summary>
    /// <param name="dtoId">The unique identifier of the doctor to update</param>
    /// <param name="dto">Data transfer object containing updated doctor details</param>
    /// <returns>DoctorDto representing the updated doctor</returns>
    /// <exception cref="ArgumentException">Thrown when doctor with specified ID is not found</exception>
    public DoctorDto Update(int dtoId, DoctorCreateUpdateDto dto)
    {
        if (!manager.Exists(dtoId))
        {
            throw new KeyNotFoundException("Entity not found");
        }

        var entity = mapper.Map<Doctor>(dto);
        entity.Id = dtoId;
        manager.Update(entity);
        return mapper.Map<DoctorDto>(entity);
    }
}