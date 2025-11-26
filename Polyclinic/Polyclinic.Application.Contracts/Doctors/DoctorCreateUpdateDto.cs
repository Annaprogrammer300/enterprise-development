using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Passport number is required")]
    [StringLength(20, ErrorMessage = "Passport number cannot exceed 20 characters")]
    string PassportNumber,

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
    string FullName,

    [Required(ErrorMessage = "Birth year is required")]
    [Range(1900, 2100, ErrorMessage = "Birth year must be between 1900 and 2100")]
    int BirthYear,

    [Required(ErrorMessage = "Specialization is required")]
    [StringLength(50, ErrorMessage = "Specialization cannot exceed 50 characters")]
    string Specialization,

    [Required(ErrorMessage = "Experience is required")]
    [Range(0, 100, ErrorMessage = "Experience must be between 0 and 100 years")]
    int Experience
);