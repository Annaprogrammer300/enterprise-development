namespace Polyclinic.Application.Contracts.Patients;

/// <summary>
/// DTO для POST/PUT запросов к пациентам
/// </summary>
/// <param name="PassportNumber">Номер паспорта пациента</param>
/// <param name="FullName">Полное имя пациента</param>
/// <param name="Gender">Пол пациента</param>
/// <param name="BirthDate">Дата рождения пациента</param>
/// <param name="Address">Адрес проживания пациента</param>
/// <param name="BloodGroup">Группа крови пациента</param>
/// <param name="RhesusFactor">Резус-фактор пациента</param>
/// <param name="Phone">Контактный телефон пациента</param>
public record PatientCreateUpdateDto(
    string? PassportNumber,
    string? FullName,
    string? Gender,
    DateTime? BirthDate,
    string? Address,
    string? BloodGroup,
    string? RhesusFactor,
    string? Phone
);