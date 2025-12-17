using Microsoft.Extensions.Logging;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;
using Polyclinic.Domain.Enum;

namespace Polyclinic.Generator.Services;

/// <summary>
/// Service for saving patients in the database
/// </summary>
public class ProducerService : IProducerService
{
    private readonly IManager<Patient, int> _patientManager;
    private readonly ILogger<ProducerService> _logger;

    public ProducerService(IManager<Patient, int> patientManager, ILogger<ProducerService> logger)
    {
        _patientManager = patientManager;
        _logger = logger;
    }

    /// <summary>
    /// Saves a batch of patients (DTO) in the database
    /// </summary>
    public Task SendAsync(IList<PatientDto> batch)
    {
        if (batch == null || batch.Count == 0)
        {
            _logger.LogWarning("An empty batch of patients was received");
            return Task.CompletedTask;
        }

        try
        {
            _logger.LogInformation("The beginning of saving the batch. Size: {count} patients", batch.Count);

            // Converting the DTO to a Domain Entity for saving to the database
            var patients = batch.Select(dto => new Patient
            {
                PassportNumber = dto.PassportNumber,
                FullName = dto.FullName,
                Gender = Enum.Parse<Gender>(dto.Gender),
                BirthDate = dto.BirthDate,
                Address = dto.Address,
                BloodGroup = Enum.Parse<BloodGroup>(dto.BloodGroup),
                RhesusFactor = Enum.Parse<RhesusFactor>(dto.RhesusFactor),
                Phone = dto.Phone
            }).ToList();

            // Saving patients in the database
            var savedCount = 0;
            foreach (var patient in patients)
            {
                _patientManager.Create(patient);
                savedCount++;
            }

            _logger.LogInformation("The batch was saved successfully. Saved patients: {count}", savedCount);

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred when saving the patient batch. Batch size: {count}", batch.Count);
            return Task.FromException(ex);
        }
    }
}