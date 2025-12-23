using Grpc.Net.Client;
using Polyclinic.Application.Contracts.Grpc;
using Polyclinic.Generator.Grpc.Client.Services;
using Polyclinic.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddSingleton(serviceProvider =>
{
    var grpcServiceUrl = builder.Configuration["Grpc:ServiceUrl"]
           ?? throw new InvalidOperationException("Grpc:ServiceUrl is not configured");

    var httpHandler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    var channel = GrpcChannel.ForAddress(grpcServiceUrl, new GrpcChannelOptions
    {
        HttpHandler = httpHandler
    });

    return new PatientStreaming.PatientStreamingClient(channel);
});

builder.Services.AddHostedService<PatientGenerationBackgroundService>();

var app = builder.Build();
app.MapDefaultEndpoints();
app.Run();