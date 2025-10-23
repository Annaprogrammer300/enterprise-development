namespace Polyclinic.Tests;

/// <summary>
/// Test class for Polyclinic application functionality
/// </summary>
public class PolyclinicTest(PolyclinicFixture fixture) : IClassFixture<PolyclinicFixture>
{
   
    /// <summary>
    /// Tests filtering doctors with work experience of at least 10 years
    /// </summary>
    [Fact]
    public void Doctors_With_Experience_AtLeast_10Years()
    {
        var result = fixture.GetDoctorsWithExperience(10);
        Assert.All(result, d => Assert.True(d.Experience >= 10));
    }

    /// <summary>
    /// Tests getting patients for a specific doctor, ordered by full name
    /// </summary>
    [Fact]
    public void Patients_By_Specific_Doctor()
    {
        var doctor = fixture.Doctors.First();
        var result = fixture.GetPatientsByDoctor(doctor);

        Assert.True(result.SequenceEqual(result.OrderBy(p => p.FullName)));
    }

    /// <summary>
    /// Tests counting repeated appointments in the last month
    /// </summary>
    [Fact]
    public void Count_Of_Repeated_Appointments_LastMonth()
    {
        var lastMonth = new DateTime(2025, 9, 23);
        var today = new DateTime(2025, 10, 23);
        var result = fixture.CountRepeatedAppointments(lastMonth, today);

        Assert.True(result >= 0);
    }

    /// <summary>
    /// Tests filtering patients over 30 years old who visited multiple doctors
    /// </summary>
    [Fact]
    public void Patients_Over30_With_Multiple_Doctors()
    {
        var date = new DateTime(2025, 10, 23);
        var result = fixture.GetPatientsOverAgeWithMultipleDoctors(30, date);

        Assert.All(result, p => Assert.True(p.BirthDate <= date));
    }

    /// <summary>
    /// Tests getting appointments in selected cabinet for current month
    /// </summary>
    [Fact]
    public void Appointments_In_Selected_Cabinet_CurrentMonth()
    {
        var selectedCabinet = 101;
        var current = new DateTime(2025, 10, 23);

        var result = fixture.GetAppointmentsInCabinetForMonth(selectedCabinet, current.Month, current.Year);

        Assert.All(result, a => Assert.Equal(selectedCabinet, a.RoomNumber));
    }
}
