namespace Polyclinic.Application.Contracts.Patients;

/// <summary>
/// The heir of the patient application service
/// </summary>
public interface IPatientService : IApplicationService<PatientDto, PatientCreateUpdateDto, int>
{
    /// <summary>
    /// Receives the patient by passport number
    /// </summary>
    /// <param name="passportNumber">Passport number</param>
    /// <returns>Patient's DTO</returns>
    public Task<PatientDto?> GetByPassportAsync(string? passportNumber);
}