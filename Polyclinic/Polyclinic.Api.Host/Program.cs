using Microsoft.OpenApi.Models;
using Polyclinic.Application;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Application.Services;
using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;
using Polyclinic.Infrastructure.EfCore;
using Polyclinic.Infrastructure.EfCore.Repositories;
using Polyclinic.ServiceDefaults;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Polyclinic API", Version = "v1" });
});

builder.AddNpgsqlDbContext<PolyclinicDbContext>("postgresDb");

builder.Services.AddScoped<IManager<Patient, int>, PatientEfCoreManager>();
builder.Services.AddScoped<IManager<Doctor, int>, DoctorEfCoreManager>();
builder.Services.AddScoped<IManager<Appointment, int>, AppointmentEfCoreManager>();

builder.Services.AddScoped<IApplicationService<PatientDto, PatientCreateUpdateDto, int>, PatientService>();
builder.Services.AddScoped<IApplicationService<DoctorDto, DoctorCreateUpdateDto, int>, DoctorService>();
builder.Services.AddScoped<IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>, AppointmentService>();

builder.Services.AddScoped<PolyclinicManager>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddAutoMapper(typeof(PolyclinicProfile));

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PolyclinicDbContext>();

    context.Database.EnsureCreated();

    await DbSeeder.SeedAllAsync(context);
}

app.Run();
