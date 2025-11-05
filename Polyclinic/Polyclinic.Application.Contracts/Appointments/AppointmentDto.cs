namespace Polyclinic.Application.Contracts.Appointments;

/// <summary>
/// DTO для GET запросов к записям на прием
/// </summary>
/// <param name="Id">Идентификатор записи</param>
/// <param name="PatientId">Идентификатор пациента</param>
/// <param name="PatientFullName">Полное имя пациента</param>
/// <param name="DoctorId">Идентификатор врача</param>
/// <param name="DoctorFullName">Полное имя врача</param>
/// <param name="DoctorSpecialization">Специализация врача</param>
/// <param name="DateTime">Дата и время приема</param>
/// <param name="RoomNumber">Номер кабинета</param>
/// <param name="IsRepeat">Признак повторного приема</param>
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