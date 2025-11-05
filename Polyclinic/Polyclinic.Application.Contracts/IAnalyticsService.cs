using Polyclinic.Application.Contracts.Appointments;

namespace Polyclinic.Application.Contracts;

/// <summary>
/// Service for analytics and reporting
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Get doctors with experience at least specified years
    /// </summary>
    public Task<List<int>> GetDoctorsWithExperienceAtLeastAsync(int minExperience);

    /// <summary>
    /// Get patients for specific doctor ordered by full name
    /// </summary>
    public Task<List<string>> GetPatientsByDoctorAsync(int doctorId);

    /// <summary>
    /// Count repeated appointments in the last month
    /// </summary>
    public Task<int> CountRepeatedAppointmentsLastMonthAsync(DateTime lastMonth, DateTime today);

    /// <summary>
    /// Get patients over specified age who visited multiple doctors
    /// </summary>
    public Task<List<DateTime>> GetPatientsOverAgeWithMultipleDoctorsAsync(int age, DateTime date);

    /// <summary>
    /// Get appointments in selected cabinet for current month
    /// </summary>
    public Task<List<AppointmentDto>> GetAppointmentsInCabinetForCurrentMonthAsync(int roomNumber, DateTime currentDate);
}