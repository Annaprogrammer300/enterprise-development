namespace Polyclinic.Models;

/// <summary>
/// The gender of the patient
/// </summary>
public enum Gender
{
    Male,
    Female
}

/// <summary>
/// The patient's blood type
/// </summary>
public enum BloodGroup
{
    I,
    II,
    III,
    IV
}

/// <summary>
/// The patient's Rh factor
/// </summary>
public enum RhesusFactor
{
    Positive,
    Negative
}

/// <summary>
/// Directory of doctors' specializations
/// </summary>
public enum Specialization
{
    Therapist,
    Cardiologist,
    Neurologist,
    Surgeon,
    Dentist,
    Pediatrician,
    Dermatologist,
    Ophthalmologist,
    Orthopedist,
    Psychiatrist
}
