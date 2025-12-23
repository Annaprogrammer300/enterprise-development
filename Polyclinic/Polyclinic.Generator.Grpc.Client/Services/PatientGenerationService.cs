using Grpc.Core;
using Microsoft.Extensions.Options;
using Polyclinic.Application.Contracts.Grpc;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Generator.Generator;
using Polyclinic.Generator.Grpc.Client.Configurations;

namespace Polyclinic.Generator.Grpc.Client.Services;

/// <summary>
/// Background service that periodically generates random patient DTOs and streams them in batches
/// to a remote gRPC patient streaming service using configurable timing options. 
/// </summary>
public class PatientGenerationBackgroundService(
    ILogger<PatientGenerationBackgroundService> logger,
    PatientStreaming.PatientStreamingClient client,
    IOptions<GeneratorOptions> options) : BackgroundService
{
    private readonly ILogger<PatientGenerationBackgroundService> _logger = logger;
    private readonly PatientStreaming.PatientStreamingClient _client = client;
    private readonly GeneratorOptions _options = options.Value;

    /// <summary>
    /// Main execution loop that runs while the hosted service is active, generating and sending
    /// patient batches until the configured generation window expires or cancellation is requested.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Patient generation background service starting");

        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

        var generationTimeout = TimeSpan.FromMinutes(1);
        var batchSize = _options.BatchSize;
        var waitTime = _options.WaitTime;

        _logger.LogInformation("Starting 1-minute patient generation. Batch size: {BatchSize}, Wait time: {WaitTime}s",
            batchSize, waitTime);

        var startTime = DateTime.UtcNow;
        var patientCount = 0;

        try
        {
            while (DateTime.UtcNow - startTime < generationTimeout && !stoppingToken.IsCancellationRequested)
            {
                var generated = await GenerateAndSendBatch(batchSize, stoppingToken);
                if (generated)
                {
                    patientCount += batchSize;
                    _logger.LogInformation("Sent batch of {BatchSize} patients. Total: {Total}", batchSize, patientCount);
                }

                await Task.Delay(TimeSpan.FromSeconds(waitTime), stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Patient generation cancelled");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during patient generation");
        }

        _logger.LogInformation("Patient generation completed. Total patients sent: {Total}", patientCount);
    }

    /// <summary>
    /// Generates a batch of patient DTOs and sends them over a bidirectional gRPC stream,
    /// logging each sent patient and processing all server responses for the batch.
    /// </summary>
    private async Task<bool> GenerateAndSendBatch(int count, CancellationToken stoppingToken)
    {
        try
        {
            var patientDtos = PatientGenerator.GeneratePatientDtos(count);

            using var call = _client.StreamPatients(
                deadline: DateTime.UtcNow.AddSeconds(_options.GrpcTimeoutSeconds),
                cancellationToken: stoppingToken);

            foreach (var patientDto in patientDtos)
            {
                var request = MapToGrpcMessage(patientDto);

                await call.RequestStream.WriteAsync(request, stoppingToken);
                _logger.LogDebug("Sent patient: {FullName}, Passport: {Passport}",
                    patientDto.FullName, patientDto.PassportNumber);
            }

            await call.RequestStream.CompleteAsync();

            var responses = 0;
            await foreach (var response in call.ResponseStream.ReadAllAsync(stoppingToken))
            {
                responses++;
                if (!response.Success)
                {
                    _logger.LogWarning("Received failure response: {Message}", response.Message);
                }
            }

            _logger.LogDebug("Batch completed with {ResponseCount} responses", responses);
            return true;
        }
        catch (RpcException rpcEx)
        {
            _logger.LogError(rpcEx, "gRPC error during batch sending: {Status}", rpcEx.Status);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during batch sending");
            return false;
        }
    }

    /// <summary>
    /// Maps a <see cref="PatientCreateUpdateDto"/> instance to a gRPC <see cref="PatientStreamMessage"/>
    /// that can be sent over the streaming client. 
    /// </summary>
    private static PatientStreamMessage MapToGrpcMessage(PatientCreateUpdateDto patientDto) => new()
    {
        PassportNumber = patientDto.PassportNumber,
        FullName = patientDto.FullName,
        Gender = patientDto.Gender,
        BirthDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(patientDto.BirthDate),
        Address = patientDto.Address,
        BloodGroup = patientDto.BloodGroup,
        RhesusFactor = patientDto.RhesusFactor,
        Phone = patientDto.Phone
    };
}