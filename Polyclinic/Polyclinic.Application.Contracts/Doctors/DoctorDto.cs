namespace Polyclinic.Application.Contracts.Doctors;

/// <summary>
/// DTO for GETTING requests to doctors
/// </summary>
/// <param name="Id">Doctor ID</param>
/// <param name="PassportNumber">Doctor's passport number</param>
/// <param name="FullName">Full name of the doctor</param>
/// <param name="BirthYear">Year of birth of the doctor</param>
/// <param name="Specialization">Specialization of the doctor</param>
/// <param name="Experience">Work experience as a doctor (in years)</param>
public record DoctorDto(
    int Id,
    string PassportNumber,
    string FullName,
    int BirthYear,
    string Specialization,
    int Experience
);