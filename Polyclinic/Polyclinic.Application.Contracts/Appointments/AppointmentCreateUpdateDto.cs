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
    int? PatientId,
    int? DoctorId,
    DateTime? DateTime,
    int? RoomNumber,
    bool? IsRepeat
);