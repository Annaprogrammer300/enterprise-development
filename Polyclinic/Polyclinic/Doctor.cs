using Polyclinic.Enum;

namespace Polyclinic;

/// <summary>
/// The class representing the polyclinic doctor
/// </summary>
public class Doctor
{
    /// <summary>
    /// The doctor's passport number
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Full name of the doctor 
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// The doctor's year of birth
    /// </summary>
    public int BirthYear { get; set; }

    /// <summary>
    /// The doctor's specialization
    /// </summary>
    public Specialization Specialization { get; set; }

    /// <summary>
    /// Doctor's work experience (in years)
    /// </summary>
    public int Experience { get; set; }
}
