using Polyclinic.Components;

namespace Polyclinic.Tests;

/// <summary>
/// Test fixture for Polyclinic tests
/// </summary>
public class PolyclinicFixture 
{
    /// <summary>
    /// List of patients for testing
    /// </summary>
    public List<Patient> Patients { get; private set; }

    /// <summary>
    /// List of doctors for testing
    /// </summary>
    public List<Doctor> Doctors { get; private set; }

    /// <summary>
    /// List of appointments for testing
    /// </summary>
    public List<Appointment> Appointments { get; private set; }

    /// <summary>
    /// Initializes test fixture with sample data
    /// </summary>
    public PolyclinicFixture()
    {
        Patients = TestDataSeeder.GetPatients();
        Doctors = TestDataSeeder.GetDoctors();
        Appointments = TestDataSeeder.GetAppointments(Patients, Doctors);
    }

    /// <summary>
    /// Gets a doctor with specified experience
    /// </summary>
    public List<Doctor> GetDoctorsWithExperience(int minExperience)
    {
        return Doctors.Where(d => d.Experience >= minExperience).ToList();
    }

    /// <summary>
    /// Gets patients for specific doctor
    /// </summary>
    public List<Patient> GetPatientsByDoctor(Doctor doctor)
    {
        return Appointments
            .Where(a => a.Doctor == doctor)
            .Select(a => a.Patient)
            .OrderBy(p => p.FullName)
            .ToList();
    }

    /// <summary>
    /// Counts repeated appointments in specified period
    /// </summary>
    public int CountRepeatedAppointments(DateTime startDate, DateTime endDate)
    {
        return Appointments
            .Count(a => a.IsRepeat && a.DateTime >= startDate && a.DateTime <= endDate);
    }

    /// <summary>
    /// Gets patients over specified age with multiple doctors
    /// </summary>
    public List<Patient> GetPatientsOverAgeWithMultipleDoctors(int age, DateTime date )
    {
        var cutoffDate = date.AddYears(-age);
        return Appointments
            .GroupBy(a => a.Patient)
            .Where(g => g.Select(a => a.Doctor).Distinct().Count() > 1)
            .Select(g => g.Key)
            .Where(p => p.BirthDate <= cutoffDate)
            .OrderBy(p => p.BirthDate)
            .ToList();
    }

    /// <summary>
    /// Gets appointments in specified cabinet and month
    /// </summary>
    public List<Appointment> GetAppointmentsInCabinetForMonth(int roomNumber, int month, int year)
    {
        return Appointments
            .Where(a => a.RoomNumber == roomNumber &&
                       a.DateTime.Month == month &&
                       a.DateTime.Year == year)
            .ToList();
    }



}

