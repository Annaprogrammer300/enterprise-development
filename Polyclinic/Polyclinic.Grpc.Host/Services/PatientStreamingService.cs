using Grpc.Core;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Grpc;
using Polyclinic.Application.Contracts.Patients;

namespace Polyclinic.Grpc.Host.Services;

/// <summary>
/// gRPC service that receives a bidirectional stream of patients, saves them via the application layer, 
/// and sends back per‑patient status responses to the client.
/// </summary>
public class PatientStreamingService(
    ILogger<PatientStreamingService> logger,
    IServiceScopeFactory scopeFactory) : PatientStreaming.PatientStreamingBase
{
    private readonly ILogger<PatientStreamingService> _logger = logger;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    /// <summary>
    /// Handles a bidirectional streaming call where the client sends patient messages and 
    /// the server responds with save status messages for each received patient.
    /// </summary>
    public override async Task StreamPatients(
        IAsyncStreamReader<PatientStreamMessage> requestStream,
        IServerStreamWriter<PatientStreamResponse> responseStream,
        ServerCallContext context)
    {
        _logger.LogInformation("Started patient streaming from {Peer}", context.Peer);

        var patientCount = 0;
        var savedCount = 0;

        try
        {
            await foreach (var request in requestStream.ReadAllAsync(context.CancellationToken))
            {
                patientCount++;
                _logger.LogInformation("Received patient #{Count}: {FullName}, Passport: {PassportNumber}",
                    patientCount, request.FullName, request.PassportNumber);

                var success = SavePatientViaService(request);

                if (success) savedCount++;

                await responseStream.WriteAsync(new PatientStreamResponse
                {
                    Success = success,
                    Message = success ?
                        $"Patient {request.FullName} saved successfully" :
                        $"Failed to save patient {request.FullName}",
                    Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
                });

                _logger.LogDebug("Sent response for patient {FullName}", request.FullName);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Patient streaming cancelled by client");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during patient streaming");
            throw;
        }

        _logger.LogInformation("Finished patient streaming. Total patients received: {Total}, saved: {Saved}",
            patientCount, savedCount);
    }

    /// <summary>
    /// Creates a scoped application service, maps the incoming gRPC message to a DTO, 
    /// and persists the patient using the application service.
    /// </summary>
    private bool SavePatientViaService(PatientStreamMessage request)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var patientService = scope.ServiceProvider
                .GetRequiredService<IApplicationService<PatientDto, PatientCreateUpdateDto, int>>();
            var patientDto = MapToPatientDto(request);

            var result = patientService.Create(patientDto);

            _logger.LogInformation("Successfully saved patient via service: {FullName}, ID: {Id}",
                result.FullName, result.Id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save patient via service for {FullName}", request.FullName);
            return false;
        }
    }

    /// <summary>
    /// Maps an incoming <see cref="PatientStreamMessage"/> from gRPC to a <see cref="PatientCreateUpdateDto"/> 
    /// used by the application layer.
    /// </summary>
    private static PatientCreateUpdateDto MapToPatientDto(PatientStreamMessage request) => new(
            PassportNumber: request.PassportNumber,
            FullName: request.FullName,
            Gender: request.Gender,
            BirthDate: request.BirthDate.ToDateTime(),
            Address: request.Address,
            BloodGroup: request.BloodGroup,
            RhesusFactor: request.RhesusFactor,
            Phone: request.Phone
        );
}