using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Polyclinic.Application.Contracts.Protos;
using Polyclinic.Generator.Generator;
using static Polyclinic.Application.Contracts.Protos.PatientGrpcService;

namespace Polyclinic.Generator.Grpc.Host.Services;

/// <summary>
/// gRPC service for streaming generated patients
/// </summary>
public class PatientGrpcGeneratorServiceImpl(
    ILogger<PatientGrpcGeneratorServiceImpl> logger,
    IConfiguration configuration) : PatientGrpcServiceBase
{
    private readonly ILogger<PatientGrpcGeneratorServiceImpl> _logger = logger;
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// Sends a patient stream to the client (server streaming RPC)
    /// </summary>
    public override async Task PatientGetStream(
        Empty request,
        IServerStreamWriter<PatientListResponse> responseStream,
        ServerCallContext context)
    {
        _logger.LogInformation("PatientGrpcGeneratorService: start of patient flow transfer");

        try
        {
            // Reading the generation parameters from the configuration
            var config = _configuration.GetSection("Generator");
            var batchSize = int.Parse(config["BatchSize"] ?? "10");
            var payloadLimit = int.Parse(config["PayloadLimit"] ?? "100");
            var waitTime = int.Parse(config["WaitTime"] ?? "2");

            _logger.LogInformation(
                "PatientGrpcGeneratorService: параметры - батч: {batch}, всего: {total}, ожидание: {wait}s",
                batchSize, payloadLimit, waitTime);

            var counter = 0;

            while (counter < payloadLimit && !context.CancellationToken.IsCancellationRequested)
            {
                // Generating a batch of patients
                var patients = PatientGenerator.GeneratePatients(batchSize);

                // Converting the response to protobuf
                var response = new PatientListResponse();
                foreach (var patient in patients)
                {
                    response.Patients.Add(new PatientResponse
                    {
                        Id = patient.Id,
                        FullName = patient.FullName,
                        PassportNumber = patient.PassportNumber,
                        Address = patient.Address,
                        Phone = patient.Phone,
                        BirthDate = patient.BirthDate.ToString("yyyy-MM-dd")
                    });
                }

                // Sending the batch to the stream
                response.IsFinal = (counter + batchSize >= payloadLimit);
                await responseStream.WriteAsync(response, context.CancellationToken);

                _logger.LogInformation(
                    "PatientGrpcGeneratorService: отправлен батч {current}/{total}",
                    counter + batchSize, payloadLimit);

                counter += batchSize;

                // Waiting between batches
                if (!response.IsFinal)
                {
                    await Task.Delay(waitTime * 1000, context.CancellationToken);
                }
            }

            _logger.LogInformation("PatientGrpcGeneratorService: the flow is completed successfully");
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("PatientGrpcGeneratorService: the flow is cancelled by the client");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PatientGrpcGeneratorService: error when transmitting the stream");
            throw;
        }
    }
}