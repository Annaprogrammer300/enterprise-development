namespace Polyclinic.Models;

/// <summary>
/// A class that describes a patient's appointment with a doctor
/// </summary>
public class Appointment
{
    /// <summary>
    /// The patient who has an appointment
    /// </summary>
    public required Patient Patient { get; set; }

    /// <summary>
    /// The doctor who sees the patient
    /// </summary>
    public required Doctor Doctor { get; set; }

    /// <summary>
    /// Date and time of the appointment
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// The number of the room where the appointment is held
    /// </summary>
    public int RoomNumber { get; set; }

    /// <summary>
    /// A sign of whether the appointment is repeated
    /// </summary>
    public bool IsRepeat { get; set; }
}
