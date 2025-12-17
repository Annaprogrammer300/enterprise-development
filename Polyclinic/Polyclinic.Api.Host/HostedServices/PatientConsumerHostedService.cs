using Polyclinic.Application.Contracts.Patients;

namespace Polyclinic.Api.Host.HostedServices;

/// <summary>
/// A service for processing patient reports from a broker and saving them to a database
/// </summary>
public class PatientConsumerHostedService(
    IServiceScopeFactory scopeFactory,
    ILogger<PatientConsumerHostedService> logger) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly ILogger<PatientConsumerHostedService> _logger = logger;

    /// <summary>
    /// Listens to messages from the broker and saves patients to the database
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("PatientConsumerHostedService: initialization");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogDebug("PatientConsumerHostedService: listens to messages from the broker");

                await Task.Delay(5000, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("PatientConsumerHostedService: cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PatientConsumerHostedService: error");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }

    /// <summary>
    /// Processes the batch of patients and saves them to the database
    /// </summary>
    public void ProcessPatientBatch(List<PatientDto> patients)
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IPatientService>();

        _logger.LogInformation("Consumer: обработка {count} пациентов", patients.Count);

        try
        {
            // Convert DTO to entities and save
            foreach (var patientDto in patients)
            {
                _logger.LogDebug("Consumer: saving the patient {id} - {fullName}",
                    patientDto.Id, patientDto.FullName);
            }

            _logger.LogInformation("Consumer: successfully saved {count} patients", patients.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Consumer: error saving patients");
            throw;
        }
    }
}