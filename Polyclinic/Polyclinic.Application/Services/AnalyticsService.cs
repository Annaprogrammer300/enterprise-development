using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Interfaces;


namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для аналитики
/// </summary>
public class AnalyticsService : IAnalyticsService
{
    private readonly IPolyclinicManager _polyclinicManager;
    private readonly IMapper _mapper;

    public AnalyticsService(IPolyclinicManager polyclinicRepository, IMapper mapper)
    {
        _polyclinicRepository = polyclinicRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<List<int>> GetDoctorsWithExperienceAtLeastAsync(int minExperience)
    {
        return await _polyclinicRepository.GetDoctorsWithExperienceAtLeastAsync(minExperience);
    }

    /// <inheritdoc/>
    public async Task<List<string>> GetPatientsByDoctorAsync(int doctorId)
    {
        return await _polyclinicRepository.GetPatientsByDoctorAsync(doctorId);
    }

    /// <inheritdoc/>
    public async Task<int> CountRepeatedAppointmentsLastMonthAsync(DateTime lastMonth, DateTime today)
    {
        return await _polyclinicRepository.CountRepeatedAppointmentsLastMonthAsync(lastMonth, today);
    }

    /// <inheritdoc/>
    public async Task<List<DateTime>> GetPatientsOverAgeWithMultipleDoctorsAsync(int age, DateTime date)
    {
        return await _polyclinicRepository.GetPatientsOverAgeWithMultipleDoctorsAsync(age, date);
    }

    /// <inheritdoc/>
    public async Task<List<AppointmentDto>> GetAppointmentsInCabinetForCurrentMonthAsync(int roomNumber, DateTime currentDate)
    {
        var appointments = await _polyclinicRepository.GetAppointmentsInCabinetForCurrentMonthAsync(roomNumber, currentDate);
        return _mapper.Map<List<AppointmentDto>>(appointments);
    }
}