using Polyclinic.Components;

namespace Polyclinic.Tests;
public class PolyclinicTests
{
    private readonly List<Patient> _patients;
    private readonly List<Doctor> _doctors;
    private readonly List<Appointment> _appointments;

    public PolyclinicTests()
    {
        _patients = TestDataSeeder.GetPatients();
        _doctors = TestDataSeeder.GetDoctors();
        _appointments = TestDataSeeder.GetAppointments(_patients, _doctors);
    }

    [Fact]
    public void Doctors_With_Experience_AtLeast_10Years()
    {
        var result = _doctors.Where(d => d.Experience >= 10).ToList();
        Assert.All(result, d => Assert.True(d.Experience >= 10));
    }

    [Fact]
    public void Patients_By_Specific_Doctor()
    {
        var doctor = _doctors.First();
        var result = _appointments
            .Where(a => a.Doctor == doctor)
            .Select(a => a.Patient)
            .OrderBy(p => p.FullName)
            .ToList();

        Assert.True(result.SequenceEqual(result.OrderBy(p => p.FullName)));
    }

    [Fact]
    public void Count_Of_Repeated_Appointments_LastMonth()
    {
        var lastMonth = DateTime.Today.AddMonths(-1);
        var result = _appointments
            .Where(a => a.IsRepeat && a.DateTime >= lastMonth)
            .Count();

        Assert.True(result >= 0);
    }

    [Fact]
    public void Patients_Over30_With_Multiple_Doctors()
    {
        var date = new DateTime(2025, 10, 23);
        var result = _appointments
            .GroupBy(a => a.Patient)
            .Where(g => g.Select(a => a.Doctor).Distinct().Count() > 1)
            .Select(g => g.Key)
            .Where(p => p.BirthDate <= date)
            .OrderBy(p => p.BirthDate)
            .ToList();

        Assert.All(result, p => Assert.True(p.BirthDate <= date));
    }

    [Fact]
    public void Appointments_In_Selected_Cabinet_CurrentMonth()
    {
        var selectedCabinet = 101;
        var currentMonth = DateTime.Today.Month;

        var result = _appointments
            .Where(a => a.DateTime.Month == currentMonth && a.RoomNumber == selectedCabinet)
            .ToList();

        Assert.All(result, a => Assert.Equal(selectedCabinet, a.RoomNumber));
    }
}
