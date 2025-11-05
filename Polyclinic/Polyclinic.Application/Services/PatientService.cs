using AutoMapper;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Application.Interfaces;

namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для работы с пациентами
/// </summary>
public class PatientService : IPatientService
{
    private readonly IPatientManager _patientManager;
    private readonly IMapper _mapper;

    public PatientService(IPatientManager patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<List<PatientDto>> GetAsync()
    {
        var patients = await _patientRepository.GetAllAsync();
        return _mapper.Map<List<PatientDto>>(patients);
    }

    /// <inheritdoc/>
    public async Task<PatientDto?> GetAsync(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        return _mapper.Map<PatientDto?>(patient);
    }

    /// <inheritdoc/>
    public async Task<PatientDto?> GetByPassportAsync(string? passportNumber)
    {
        if (string.IsNullOrEmpty(passportNumber))
            return null;

        var patient = await _patientRepository.GetByPassportAsync(passportNumber);
        return _mapper.Map<PatientDto?>(patient);
    }

    /// <inheritdoc/>
    public async Task<PatientDto> CreateAsync(PatientCreateUpdateDto dto)
    {
        var patient = _mapper.Map<Patient>(dto);
        var created = await _patientRepository.CreateAsync(patient);
        return _mapper.Map<PatientDto>(created);
    }

    /// <inheritdoc/>
    public async Task<PatientDto> UpdateAsync(int id, PatientCreateUpdateDto dto)
    {
        var patient = _mapper.Map<Patient>(dto);
        patient.Id = id;
        var updated = await _patientRepository.UpdateAsync(patient);
        return _mapper.Map<PatientDto>(updated);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        await _patientRepository.DeleteAsync(id);
    }
}