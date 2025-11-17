using Polyclinic.Application.Contracts.Appointments;

namespace Polyclinic.Application.Contracts;

/// <summary>
/// The interface of the analytics service
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Getting doctors with at least the specified work experience
    /// </summary>
    /// <param name="minExperience">Minimum work experience</param>
    /// <returns></returns>
    public List<int> GetDoctorsWithExperienceAtLeast(int minExperience);

    /// <summary>
    /// Receiving patients by the specified doctor
    /// </summary>
    /// <param name="DoctorID">Doctor's ID</param>
    /// <returns></returns>
    public List<string> GetPatientsByDoctor(int doctorId);

    /// <summary>
    /// Counting repeat admissions over the past month
    /// </summary>
    /// <param name="lastMonth">Date of the beginning of the last month</param>
    /// <param name="today">Current date</param>
    /// <returns></returns>
    public int CountRepeatedAppointmentsLastMonth(DateTime lastMonth, DateTime today);

    /// <summary>
    /// Receiving patients older than the specified age who have visited several doctors
    /// </summary>
    /// <param name="age">Minimum age</param>
    /// <param name="date">Current date for age calculation</param>
    /// <returns></returns>
    public List<DateTime> GetPatientsOverAgeWithMultipleDoctors(int age, DateTime date);

    /// <summary>
    /// Receiving appointments in the specified office for the current month
    /// </summary>
    /// <param name="roomNumber">Cabinet number</param>
    /// <param name="currentDate">Current date</param>
    /// <returns></returns>
    public List<AppointmentDto> GetAppointmentsInCabinetForCurrentMonth(int roomNumber, DateTime currentDate);
}