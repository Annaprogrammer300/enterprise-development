using Microsoft.OpenApi.Models;
using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;
using Polyclinic.Application;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Application.Services;
using Polyclinic.Infrastructure.InMemory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Polyclinic API", Version = "v1" });
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(PolyclinicProfile));

// Register infrastructure services
builder.Services.AddSingleton<IManager<Patient, int>, PatientInMemoryManager>();
builder.Services.AddSingleton<IManager<Doctor, int>, DoctorInMemoryManager>();
builder.Services.AddSingleton<IManager<Appointment, int>, AppointmentInMemoryManager>();

// Register application services
builder.Services.AddScoped<IApplicationService<PatientDto, PatientCreateUpdateDto, int>, PatientService>();
builder.Services.AddScoped<IApplicationService<DoctorDto, DoctorCreateUpdateDto, int>, DoctorService>();
builder.Services.AddScoped<IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>, AppointmentService>();

// Register domain service
builder.Services.AddScoped<PolyclinicManager>();

// Register analytics service
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();