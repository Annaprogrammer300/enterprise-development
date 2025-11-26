using System.ComponentModel.DataAnnotations;

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
[Required(ErrorMessage = "Passport number is required")]
    [StringLength(20, ErrorMessage = "Passport number cannot exceed 20 characters")]
    string PassportNumber,

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
    string FullName,

    [Required(ErrorMessage = "Gender is required")]
    [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters")]
    string Gender,

    [Required(ErrorMessage = "Birth date is required")]
    DateTime BirthDate,

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
    string Address,

    [Required(ErrorMessage = "Blood group is required")]
    [StringLength(5, ErrorMessage = "Blood group cannot exceed 5 characters")]
    string BloodGroup,

    [Required(ErrorMessage = "Rhesus factor is required")]
    [StringLength(5, ErrorMessage = "Rhesus factor cannot exceed 5 characters")]
    string RhesusFactor,

    [Required(ErrorMessage = "Phone is required")]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    string Phone
);