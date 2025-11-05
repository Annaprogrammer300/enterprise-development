namespace Polyclinic.Application.Contracts.Patients;

/// <summary>
/// DTO for GET requests to patients
/// </summary>
/// <param name="Id">Patient ID</param>
/// <param name="PassportNumber">Patient passport number</param>
/// <param name="FullName">Patient full name</param>
/// <param name="Gender">Patient gender</param>
/// <param name="BirthDate">Patient's date of birth</param>
/// <param name="Address">Patient's residential address</param>
/// <param name="BloodGroup">Patient's blood group</param>
/// <param name="RhesusFactor">Patient's Rh factor</param>
/// <param name="Phone">Patient's contact phone number</param>
public record PatientDto(
    int Id,
    string PassportNumber,
    string FullName,
    string Gender,
    DateTime BirthDate,
    string Address,
    string BloodGroup,
    string RhesusFactor,
    string Phone
);