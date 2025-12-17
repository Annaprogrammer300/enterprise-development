namespace Polyclinic.Application.Contracts.Patients;

/// <summary>
/// DTO for GET requests to patients (used in API responses)
/// Must match the properties of the Domain Entity Patient
/// </summary>
public class PatientDto
{
    /// <summary>
    /// The unique identifier of the patient
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Patient's passport number
    /// </summary>
    public string PassportNumber { get; set; } = string.Empty;

    /// <summary>
    /// Patient's full name
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Patient's gender
    /// </summary>
    public string Gender { get; set; } = string.Empty;

    /// <summary>
    /// Patient's date of birth
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Residential address
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Blood type
    /// </summary>
    public string BloodGroup { get; set; } = string.Empty;

    /// <summary>
    /// The Rh factor
    /// </summary>
    public string RhesusFactor { get; set; } = string.Empty;

    /// <summary>
    /// Phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Кconstructor without parameters
    /// </summary>
    public PatientDto()
    {
    }

    /// <summary>
    /// Constructor with all parameters
    /// </summary>
    public PatientDto(int id, string passportNumber, string fullName, string gender,
        DateTime birthDate, string address, string bloodGroup, string rhesusFactor, string phone)
    {
        Id = id;
        PassportNumber = passportNumber;
        FullName = fullName;
        Gender = gender;
        BirthDate = birthDate;
        Address = address;
        BloodGroup = bloodGroup;
        RhesusFactor = rhesusFactor;
        Phone = phone;
    }
}