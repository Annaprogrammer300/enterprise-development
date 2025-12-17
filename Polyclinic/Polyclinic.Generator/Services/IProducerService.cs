using Polyclinic.Application.Contracts.Patients;

namespace Polyclinic.Generator.Services;

/// <summary>
/// Interface for the patient sending/saving service
/// </summary>
public interface IProducerService
{
    /// <summary>
    /// Sends a batch of patients (DTO) for saving to the database
    /// </summary>
    /// <param name="batch">Batch of patients in DTO format</param>
    /// <returns>Completed task</returns>
    public Task SendAsync(IList<PatientDto> batch);
}