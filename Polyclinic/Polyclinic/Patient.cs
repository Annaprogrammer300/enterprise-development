using Polyclinic.Enum;

namespace Polyclinic;

/// <summary>
/// The class representing the patient of the polyclinic
/// </summary>
public class Patient
{
    /// <summary>
    /// Unique identifier for the patient
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The patient's passport number
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Full name of the patient
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// The gender of the patient
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Patient's date of birth
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// The patient's residential address.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Patient's blood type
    /// </summary>
    public BloodGroup BloodGroup { get; set; }

    /// <summary>
    /// The patient's Rh factor
    /// </summary>
    public RhesusFactor RhesusFactor { get; set; }

    /// <summary>
    /// The patient's contact phone number
    /// </summary>
    public required string Phone { get; set; }

}
