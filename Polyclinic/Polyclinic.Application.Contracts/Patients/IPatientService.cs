namespace Polyclinic.Application.Contracts.Patients;

/// <summary>
/// Наследник аппликейшен службы для пациентов
/// </summary>
public interface IPatientService : IApplicationService<PatientDto, PatientCreateUpdateDto, int>
{
    /// <summary>
    /// Получает пациента по номеру паспорта
    /// </summary>
    /// <param name="passportNumber">Номер паспорта</param>
    /// <returns>DTO пациента</returns>
    public Task<PatientDto?> GetByPassportAsync(string? passportNumber);
}