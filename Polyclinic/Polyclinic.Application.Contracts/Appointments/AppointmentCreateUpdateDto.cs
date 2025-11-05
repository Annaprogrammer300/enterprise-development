namespace Polyclinic.Application.Contracts.Appointments;

/// <summary>
/// DTO для POST/PUT запросов к записям на прием
/// </summary>
/// <param name="PatientId">Идентификатор пациента</param>
/// <param name="DoctorId">Идентификатор врача</param>
/// <param name="DateTime">Дата и время приема</param>
/// <param name="RoomNumber">Номер кабинета</param>
/// <param name="IsRepeat">Признак повторного приема</param>
public record AppointmentCreateUpdateDto(
    int? PatientId,
    int? DoctorId,
    DateTime? DateTime,
    int? RoomNumber,
    bool? IsRepeat
);