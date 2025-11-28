using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Patients;

namespace Polyclinic.Application.Services;

/// <summary>
/// Analytical Service
/// </summary>
/// <param name="polyclinicManager">Domain service for launching polyclinic user cases</param>
/// <param name="mapper">Mapping profile</param>
public class AnalyticsService(PolyclinicManager polyclinicManager, IMapper mapper) : IAnalyticsService
{
    /// <summary>
    /// Retrieves doctors with experience greater than or equal to the specified minimum
    /// </summary>
    /// <param name="minExperience">Minimum years of experience required</param>
    /// <returns>Sorted list of doctor IDs meeting the experience criteria</returns>
    public List<DoctorDto> GetDoctorsWithExperienceAtLeast(int minExperience) =>
        mapper.Map<List<DoctorDto>>(polyclinicManager.GetDoctorsWithExperienceAtLeast(minExperience));

    /// <summary>
    /// Retrieves patient names associated with a specific doctor
    /// </summary>
    /// <param name="doctorId">The unique identifier of the doctor</param>
    /// <returns>Alphabetically sorted list of patient full names</returns>
    public List<PatientDto> GetPatientsByDoctor(int doctorId) =>
        mapper.Map<List<PatientDto>>(polyclinicManager.GetPatientsByDoctor(doctorId));

    /// <summary>
    /// Counts the number of repeated appointments within the specified date range
    /// </summary>
    /// <param name="lastMonth">Start date of the period</param>
    /// <param name="today">End date of the period</param>
    /// <returns>Number of repeated appointments in the date range</returns>
    public int CountRepeatedAppointmentsLastMonth(DateTime lastMonth, DateTime today) =>
        polyclinicManager.CountRepeatedAppointmentsLastMonth(lastMonth, today);

    /// <summary>
    /// Retrieves birth dates of patients over specified age who have visited multiple doctors
    /// </summary>
    /// <param name="age">Minimum age of patients</param>
    /// <param name="date">Reference date for age calculation</param>
    /// <returns>Sorted list of birth dates meeting the criteria</returns>
    public List<PatientDto> GetPatientsOverAgeWithMultipleDoctors(int age, DateTime date) =>
       mapper.Map<List<PatientDto>>(polyclinicManager.GetPatientsOverAgeWithMultipleDoctors(age, date));

    /// <summary>
    /// Retrieves appointments in a specific room for the current month and maps them to DTOs
    /// </summary>
    /// <param name="roomNumber">Room number to filter appointments</param>
    /// <param name="currentDate">Reference date for month filtering</param>
    /// <returns>List of appointment DTOs in the specified room for the current month</returns>
    public List<AppointmentDto> GetAppointmentsInCabinetForCurrentMonth(int roomNumber, DateTime currentDate) =>
        mapper.Map<List<AppointmentDto>>(polyclinicManager.GetAppointmentsInCabinetForCurrentMonth(roomNumber, currentDate));
}