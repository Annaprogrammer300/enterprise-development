using Polyclinic.Generator.Grpc.Host.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddLogging();

builder.Services.AddScoped<PatientGrpcGeneratorServiceImpl>();

var app = builder.Build();

app.MapGrpcService<PatientGrpcGeneratorServiceImpl>();

app.MapGet("/", () => "gRPC Generator Service running");
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("gRPC Generator Host запущен на порту 5051");

app.Run();
