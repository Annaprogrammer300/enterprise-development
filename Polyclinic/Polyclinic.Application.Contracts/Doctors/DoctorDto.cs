namespace Polyclinic.Application.Contracts.Doctors;

/// <summary>
/// DTO для GET запросов к врачам
/// </summary>
/// <param name="Id">Идентификатор врача</param>
/// <param name="PassportNumber">Номер паспорта врача</param>
/// <param name="FullName">Полное имя врача</param>
/// <param name="BirthYear">Год рождения врача</param>
/// <param name="Specialization">Специализация врача</param>
/// <param name="Experience">Опыт работы врача (в годах)</param>
public record DoctorDto(
    int Id,
    string PassportNumber,
    string FullName,
    int BirthYear,
    string Specialization,
    int Experience
);