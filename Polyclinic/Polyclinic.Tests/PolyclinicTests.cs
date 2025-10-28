namespace Polyclinic.Domain;

/// <summary>
/// Test class for Polyclinic application functionality
/// </summary>
public class PolyclinicTest(PolyclinicFixture fixture) : IClassFixture<PolyclinicFixture>
{
   
    /// <summary>
    /// Tests filtering doctors with work experience of at least 10 years
    /// </summary>
    [Fact]
    public void DoctorsWithExperienceAtLeast10Years()
    {
        List<int> expectedDoctors = [1, 2, 4, 5, 7, 8, 10];
        var result = fixture.GetDoctorsWithExperience(10);

        Assert.Equal(expectedDoctors, result);
    }

    /// <summary>
    /// Tests getting patients for a specific doctor, ordered by full name
    /// </summary>
    [Fact]
    public void PatientsBySpecificDoctor()
    {
        List<string> expectedPatients = ["Орлов Дмитрий", "Смирнова Мария"];
        var doctor = 5;
        var result = fixture.GetPatientsByDoctor(doctor);

        Assert.Equal(expectedPatients, result);
    }

    /// <summary>
    /// Tests counting repeated appointments in the last month
    /// </summary>
    [Fact]
    public void CountOfRepeatedAppointmentsLastMonth()
    {
        var expectedCount = 1;
        var lastMonth = new DateTime(2025, 9, 23);
        var today = new DateTime(2025, 10, 23);
        var result = fixture.CountRepeatedAppointments(lastMonth, today);

        Assert.Equal(expectedCount, result);
    }

    /// <summary>
    /// Tests filtering patients over 30 years old who visited multiple doctors
    /// </summary>
    [Fact]
    public void PatientsOver30WithMultipleDoctors()
    {
        List<DateTime> expectedPatients = [new DateTime(1992, 11, 11)];
        var date = new DateTime(2025, 10, 23);
        var result = fixture.GetPatientsOverAgeWithMultipleDoctors(30, date);

        Assert.Equal(expectedPatients, result);
    }

    /// <summary>
    /// Tests getting appointments in selected cabinet for current month
    /// </summary>
    [Fact]
    public void AppointmentsInSelectedCabinetCurrentMonth()
    {
        var selectedCabinet = 103;
        var current = new DateTime(2025, 10, 23);
        List<Appointment> expectedAppointment = [fixture.Appointments.First(a => a.Id == 3)];
        var result = fixture.GetAppointmentsInCabinetForMonth(selectedCabinet, current.Month, current.Year);

        Assert.Equal(expectedAppointment, result);
    }
}
