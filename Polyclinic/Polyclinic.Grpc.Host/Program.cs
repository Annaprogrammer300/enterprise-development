using Polyclinic.Application;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Application.Services;
using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;
using Polyclinic.Grpc.Host.Services;
using Polyclinic.Infrastructure.EfCore;
using Polyclinic.Infrastructure.EfCore.Repositories;
using Polyclinic.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<PolyclinicDbContext>("postgresDb");

builder.Services.AddScoped<IManager<Patient, int>, PatientEfCoreManager>();
builder.Services.AddScoped<IApplicationService<PatientDto, PatientCreateUpdateDto, int>, PatientService>();

builder.Services.AddAutoMapper(typeof(PolyclinicProfile));

builder.Services.AddGrpc();

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGrpcService<PatientStreamingService>();

app.Run();