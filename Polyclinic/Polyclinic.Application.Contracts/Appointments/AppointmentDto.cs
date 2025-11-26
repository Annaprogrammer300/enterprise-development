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
    int Id,
    int PatientId,
    string PatientFullName,
    int DoctorId,
    string DoctorFullName,
    string DoctorSpecialization,
    DateTime DateTime,
    int RoomNumber,
    bool IsRepeat
);