using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Application.Contracts.Appointments;

/// <summary>
/// DTO for POST/PUT requests to appointment appointments
/// </summary>
/// <param name="patientID">Patient ID</param>
/// <param name="DoctorID">Doctor ID</param>
/// <param name="DateTime">Date and time of reception</param>
/// <param name="roomNumber">Cabinet number</param>
/// <param name="IsRepeat">Indication of repeat admission</param>
public record AppointmentCreateUpdateDto(
    [Required(ErrorMessage = "PatientId is required")] int PatientId,
    [Required(ErrorMessage = "DoctorId is required")] int DoctorId,
    [Required(ErrorMessage = "DateTime is required")] DateTime DateTime,
    [Required][Range(1, 1000, ErrorMessage = "Room number must be between 1 and 1000")] int RoomNumber,
    [Required] bool IsRepeat
);