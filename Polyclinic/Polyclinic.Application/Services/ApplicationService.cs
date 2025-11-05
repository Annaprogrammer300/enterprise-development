using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Appointments;

namespace Polyclinic.Application.Services;

/// <summary>
/// Главный сервис приложения
/// </summary>
public class ApplicationService : IApplicationService
{
    public ApplicationService(
        IPatientService patients,
        IDoctorService doctors,
        IAppointmentService appointments,
        IAnalyticsService analytics)
    {
        Patients = patients;
        Doctors = doctors;
        Appointments = appointments;
        Analytics = analytics;
    }

    /// <inheritdoc/>
    public IPatientService Patients { get; }

    /// <inheritdoc/>
    public IDoctorService Doctors { get; }

    /// <inheritdoc/>
    public IAppointmentService Appointments { get; }

    /// <inheritdoc/>
    public IAnalyticsService Analytics { get; }
}