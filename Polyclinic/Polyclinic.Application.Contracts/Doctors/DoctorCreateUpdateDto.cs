namespace Polyclinic.Application.Contracts.Doctors;

/// <summary>
/// DTO for POST/PUT requests to doctors
/// </summary>
/// <param name="PassportNumber">Doctor's passport number</param>
/// <param name="FullName">Full name of the doctor</param>
/// <param name="BirthYear">Year of birth of the doctor</param>
/// <param name="Specialization">Specialization of the doctor</param>
/// <param name="Experience">Work experience as a doctor (in years)</param>
public record DoctorCreateUpdateDto(
    string? PassportNumber,
    string? FullName,
    int? BirthYear,
    string? Specialization,
    int? Experience
);