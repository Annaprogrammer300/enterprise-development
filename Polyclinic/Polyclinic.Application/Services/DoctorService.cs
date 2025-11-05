using AutoMapper;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Enum;
using Polyclinic.Application.Interfaces;

namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для работы с врачами
/// </summary>
public class DoctorService : IDoctorService
{
    private readonly IDoctorManager _doctorManager;
    private readonly IMapper _mapper;

    public DoctorService(IDoctorManager doctorRepository, IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<List<DoctorDto>> GetAsync()
    {
        var doctors = await _doctorRepository.GetAllAsync();
        return _mapper.Map<List<DoctorDto>>(doctors);
    }

    /// <inheritdoc/>
    public async Task<DoctorDto?> GetAsync(int id)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id);
        return _mapper.Map<DoctorDto?>(doctor);
    }

    /// <inheritdoc/>
    public async Task<List<DoctorDto>> GetBySpecializationAsync(Specialization? specialization)
    {
        if (specialization == null)
            return new List<DoctorDto>();

        var doctors = await _doctorRepository.GetBySpecializationAsync(specialization.Value);
        return _mapper.Map<List<DoctorDto>>(doctors);
    }

    /// <inheritdoc/>
    public async Task<List<DoctorDto>> GetWithMinExperienceAsync(int? minExperience)
    {
        if (minExperience == null)
            return new List<DoctorDto>();

        var doctors = await _doctorRepository.GetWithMinExperienceAsync(minExperience.Value);
        return _mapper.Map<List<DoctorDto>>(doctors);
    }

    /// <inheritdoc/>
    public async Task<DoctorDto> CreateAsync(DoctorCreateUpdateDto dto)
    {
        var doctor = _mapper.Map<Doctor>(dto);
        var created = await _doctorRepository.CreateAsync(doctor);
        return _mapper.Map<DoctorDto>(created);
    }

    /// <inheritdoc/>
    public async Task<DoctorDto> UpdateAsync(int id, DoctorCreateUpdateDto dto)
    {
        var doctor = _mapper.Map<Doctor>(dto);
        doctor.Id = id;
        var updated = await _doctorRepository.UpdateAsync(doctor);
        return _mapper.Map<DoctorDto>(updated);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        await _doctorRepository.DeleteAsync(id);
    }
}