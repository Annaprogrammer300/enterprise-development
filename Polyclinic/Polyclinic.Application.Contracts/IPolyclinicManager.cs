using Polyclinic.Domain;
using Polyclinic.Infrastructure.InMemory;

namespace Polyclinic.Application.Contracts;

/// <summary>
/// Доменная служба для имплементации бизнес-логики, связанной с поликлиникой
/// </summary>
/// <param name="patients">Менеджер пациентов</param>
/// <param name="doctors">Менеджер врачей</param>
/// <param name="appointments">Менеджер записей на прием</param>
public class PolyclinicManager(
    IManager<Patient, int> patients,
    IManager<Doctor, int> doctors,
    IManager<Appointment, int> appointments)
{
    /// <inheritdoc/>
    public List<int> GetDoctorsWithExperienceAtLeast(int minExperience)
    {
        return [.. doctors.ReadAll()
            .Where(d => d.Experience >= minExperience)
            .Select(d => d.Id)
            .Order()];
    }

    /// <inheritdoc/>
    public List<string> GetPatientsByDoctor(int doctorId)
    {
        return [.. appointments.ReadAll()
            .Where(a => a.Doctor.Id == doctorId)
            .Select(a => a.Patient.FullName)
            .OrderBy(name => name)];
    }

    /// <inheritdoc/>
    public int CountRepeatedAppointmentsLastMonth(DateTime lastMonth, DateTime today)
    {
        return appointments.ReadAll()
            .Count(a => a.IsRepeat && a.DateTime >= lastMonth && a.DateTime <= today);
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public List<Appointment> GetAppointmentsInCabinetForCurrentMonth(int roomNumber, DateTime currentDate)
    {
        return [.. appointments.ReadAll()
            .Where(a => a.RoomNumber == roomNumber &&
                       a.DateTime.Month == currentDate.Month &&
                       a.DateTime.Year == currentDate.Year)];
    }

    /// <summary>
    /// Получает пациентов по группе крови
    /// </summary>
    /// <param name="bloodGroup">Группа крови</param>
    /// <param name="rhesusFactor">Резус-фактор</param>
    /// <returns>Список пациентов</returns>
    public List<Patient> GetPatientsByBloodGroup(string bloodGroup, string rhesusFactor)
    {
        return [.. patients.ReadAll()
            .Where(p => p.BloodGroup.ToString() == bloodGroup &&
                       p.RhesusFactor.ToString() == rhesusFactor)];
    }

    /// <summary>
    /// Получает пациентов старше указанного возраста
    /// </summary>
    /// <param name="age">Минимальный возраст</param>
    /// <param name="currentDate">Текущая дата</param>
    /// <returns>Список пациентов</returns>
    public List<Patient> GetPatientsOlderThan(int age, DateTime currentDate)
    {
        var cutoffDate = currentDate.AddYears(-age);
        return [.. patients.ReadAll()
            .Where(p => p.BirthDate <= cutoffDate)];
    }
}