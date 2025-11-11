using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;

namespace Polyclinic.Application.Services;

/// <summary>
/// Аналитическая служба
/// </summary>
/// <param name="polyclinicManager">Доменная служба для запуска юзкейсов поликлиники</param>
/// <param name="mapper">Профиль маппинга</param>
public class AnalyticsService(PolyclinicManager polyclinicManager, IMapper mapper) : IAnalyticsService
{
    /// <inheritdoc/>
    public List<int> GetDoctorsWithExperienceAtLeast(int minExperience) =>
        polyclinicManager.GetDoctorsWithExperienceAtLeast(minExperience);

    /// <inheritdoc/>
    public List<string> GetPatientsByDoctor(int doctorId) =>
        polyclinicManager.GetPatientsByDoctor(doctorId);

    /// <inheritdoc/>
    public int CountRepeatedAppointmentsLastMonth(DateTime lastMonth, DateTime today) =>
        polyclinicManager.CountRepeatedAppointmentsLastMonth(lastMonth, today);

    /// <inheritdoc/>
    public List<DateTime> GetPatientsOverAgeWithMultipleDoctors(int age, DateTime date) =>
        polyclinicManager.GetPatientsOverAgeWithMultipleDoctors(age, date);

    /// <inheritdoc/>
    public List<AppointmentDto> GetAppointmentsInCabinetForCurrentMonth(int roomNumber, DateTime currentDate) =>
        mapper.Map<List<AppointmentDto>>(polyclinicManager.GetAppointmentsInCabinetForCurrentMonth(roomNumber, currentDate));
}