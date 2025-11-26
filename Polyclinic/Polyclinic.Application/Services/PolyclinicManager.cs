using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;

namespace Polyclinic.Application.Services;

/// <summary>
/// Domain service for implementing business logic related to the clinic
/// </summary>
/// <param name="patients">Patient Manager</param>
/// <param name="doctors">Doctors' Manager</param>
/// <param name="appointments">Appointment Appointment Manager</param>
public class PolyclinicManager(
    IManager<Patient, int> patients,
    IManager<Doctor, int> doctors,
    IManager<Appointment, int> appointments)
{
    /// <summary>
    /// Retrieves doctors with experience greater than or equal to the specified minimum
    /// </summary>
    /// <param name="minExperience">Minimum years of experience required</param>
    /// <returns>Sorted list of doctor IDs meeting the experience criteria</returns>
    public List<int> GetDoctorsWithExperienceAtLeast(int minExperience)
    {
        return [.. doctors.ReadAll()
            .Where(d => d.Experience >= minExperience)
            .Select(d => d.Id)
            .Order()];
    }

    /// <summary>
    /// Retrieves patient names associated with a specific doctor
    /// </summary>
    /// <param name="doctorId">The unique identifier of the doctor</param>
    /// <returns>Alphabetically sorted list of patient full names</returns>
    public List<string> GetPatientsByDoctor(int doctorId)
    {
        return [.. appointments.ReadAll()
            .Where(a => a.Doctor.Id == doctorId)
            .Select(a => a.Patient.FullName)
            .OrderBy(name => name)];
    }

    /// <summary>
    /// Counts the number of repeated appointments within the specified date range
    /// </summary>
    /// <param name="lastMonth">Start date of the period</param>
    /// <param name="today">End date of the period</param>
    /// <returns>Number of repeated appointments in the date range</returns>
    public int CountRepeatedAppointmentsLastMonth(DateTime lastMonth, DateTime today)
    {
        return appointments.ReadAll()
            .Count(a => a.IsRepeat && a.DateTime >= lastMonth && a.DateTime <= today);
    }

    /// <summary>
    /// Retrieves birth dates of patients over specified age who have visited multiple doctors
    /// </summary>
    /// <param name="age">Minimum age of patients</param>
    /// <param name="date">Reference date for age calculation</param>
    /// <returns>Sorted list of birth dates meeting the criteria</returns>
    public List<DateTime> GetPatientsOverAgeWithMultipleDoctors(int age, DateTime date)
    {
        var cutoffDate = date.AddYears(-age);
        return [.. appointments.ReadAll()
            .GroupBy(a => a.Patient)
            .Where(g => g.Select(a => a.Doctor).Distinct().Count() > 1)
            .Select(g => g.Key)
            .Where(p => p.BirthDate <= cutoffDate)
            .Select(p => p.BirthDate)
            .OrderBy(birthDate => birthDate)];
    }

    /// <summary>
    /// Retrieves appointments in a specific room for the current month
    /// </summary>
    /// <param name="roomNumber">Room number to filter appointments</param>
    /// <param name="currentDate">Reference date for month filtering</param>
    /// <returns>List of appointments in the specified room for the current month</returns>
    public List<Appointment> GetAppointmentsInCabinetForCurrentMonth(int roomNumber, DateTime currentDate)
    {
        return [.. appointments.ReadAll()
            .Where(a => a.RoomNumber == roomNumber &&
                       a.DateTime.Month == currentDate.Month &&
                       a.DateTime.Year == currentDate.Year)];
    }

    /// <summary>
    /// Retrieves patients by specific blood group and rhesus factor
    /// </summary>
    /// <param name="bloodGroup">Blood group to filter patients</param>
    /// <param name="rhesusFactor">Rhesus factor to filter patients</param>
    /// <returns>List of patients matching the blood criteria</returns>
    public List<Patient> GetPatientsByBloodGroup(string bloodGroup, string rhesusFactor)
    {
        return [.. patients.ReadAll()
            .Where(p => p.BloodGroup.ToString() == bloodGroup &&
                       p.RhesusFactor.ToString() == rhesusFactor)];
    }

    /// <summary>
    /// Retrieves patients older than the specified age
    /// </summary>
    /// <param name="age">Minimum age of patients</param>
    /// <param name="currentDate">Reference date for age calculation</param>
    /// <returns>List of patients meeting the age criteria</returns>
    public List<Patient> GetPatientsOlderThan(int age, DateTime currentDate)
    {
        var cutoffDate = currentDate.AddYears(-age);
        return [.. patients.ReadAll()
            .Where(p => p.BirthDate <= cutoffDate)];
    }
}