namespace Polyclinic.Application.Contracts.Patients;

/// <summary>
/// DTO for POST/PUT requests to patients
/// </summary>
/// <param name="PassportNumber">Patient's passport number</param>
/// <param name="FullName">Patient's full name</param>
/// <param name="Gender">Patient's gender</param>
/// <param name="BirthDate">Patient's date of birth</param>
/// <param name="Address">Patient's residential address</param>
/// <param name="BloodGroup">Patient's blood type</param>
/// <param name="RhesusFactor">Patient's Rh factor</param>
/// <param name="Phone">Patient's contact phone number</param>
public record PatientCreateUpdateDto(
    string? PassportNumber,
    string? FullName,
    string? Gender,
    DateTime? BirthDate,
    string? Address,
    string? BloodGroup,
    string? RhesusFactor,
    string? Phone
);