using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Generator.Generator;
using Polyclinic.Generator.Services;

namespace Polyclinic.Api.Host.HostedServices;

/// <summary>
/// Hosted Service для фоновой генерации пациентов
/// </summary>
public class PatientGeneratorHostedService(IServiceProvider serviceProvider, ILogger<PatientGeneratorHostedService> logger) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<PatientGeneratorHostedService> _logger = logger;

    /// <summary>
    /// Performs the background task of generating patients
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("PatientGeneratorHostedService запущен");

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var producerService = scope.ServiceProvider.GetRequiredService<IProducerService>();

            // Generation Parameters
            var batchSize = 10;
            var totalPatients = 100;
            var delaySeconds = 2;
            var generated = 0;

            while (generated < totalPatients && !stoppingToken.IsCancellationRequested)
            {
                var domainPatients = PatientGenerator.GeneratePatients(batchSize);

                var patientDtos = domainPatients
                    .Select(p => new PatientDto
                    {
                        Id = 0,
                        PassportNumber = p.PassportNumber,
                        FullName = p.FullName,
                        Gender = p.Gender.ToString(),  // enum → string
                        BirthDate = p.BirthDate,
                        Address = p.Address,
                        BloodGroup = p.BloodGroup.ToString(),  // enum → string
                        RhesusFactor = p.RhesusFactor.ToString(),  // enum → string
                        Phone = p.Phone
                    })
                    .ToList();

                _logger.LogInformation("Sending a batch {current}/{total}, size {size}",
                    generated + batchSize, totalPatients, batchSize);

                await producerService.SendAsync(patientDtos);

                generated += batchSize;

                if (generated < totalPatients)
                {
                    await Task.Delay(delaySeconds * 1000, stoppingToken);
                }
            }

            _logger.LogInformation("The generation is completed. Total {total} patients created", generated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during background generation of patients");
            throw;
        }
    }
}