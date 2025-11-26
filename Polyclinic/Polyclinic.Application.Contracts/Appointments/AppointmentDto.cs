using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Application.Contracts.Appointments;

/// <summary>
/// DTO for GET requests to appointment appointments
/// </summary>
/// <param name="Id">Appointment ID</param>
/// <param name="patientID">Patient ID</param>
/// <param name="PatientFullName">Patient's full name</param>
/// <param name="DoctorID">Doctor's ID</param>
/// <param name="DoctorFullName">Full name of the doctor</param>
/// <param name="DoctorSpecialization">Specialization of the doctor</param>
/// <param name="DateTime">Date and time of reception</param>
/// <param name="roomNumber">Cabinet number</param>
/// <param name="IsRepeat">Indication of repeat admission</param>
public record AppointmentDto(
    [Required] int Id,
    [Required] int PatientId,
    [Required][StringLength(100)] string PatientFullName,
    [Required] int DoctorId,
    [Required][StringLength(100)] string DoctorFullName,
    [Required][StringLength(50)] string DoctorSpecialization,
    [Required] DateTime DateTime,
    [Required][Range(1, 1000)] int RoomNumber,
    [Required] bool IsRepeat
);