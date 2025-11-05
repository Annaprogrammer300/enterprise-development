namespace Polyclinic.Application.Contracts.Doctors;

/// <summary>
/// DTO для POST/PUT запросов к врачам
/// </summary>
/// <param name="PassportNumber">Номер паспорта врача</param>
/// <param name="FullName">Полное имя врача</param>
/// <param name="BirthYear">Год рождения врача</param>
/// <param name="Specialization">Специализация врача</param>
/// <param name="Experience">Опыт работы врача (в годах)</param>
public record DoctorCreateUpdateDto(
    string? PassportNumber,
    string? FullName,
    int? BirthYear,
    string? Specialization,
    int? Experience
);