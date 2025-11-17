namespace Polyclinic.Application.Contracts.Appointments;

/// <summary>
/// The heir of the application service for appointment appointments
/// </summary>
public interface IAppointmentService : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    /// <summary>
    /// Gets patient records
    /// </summary>
    /// <param name="patientID">Patient ID</param>
    /// <returns>List of DTO records</returns>
    public Task<List<AppointmentDto>> GetByPatientAsync(int? patientId);

    /// <summary>
    /// Gets doctor's notes
    /// </summary>
    /// <param name="DoctorID">Doctor's ID</param>
    /// <returns>List of DTO records</returns>
    public Task<List<AppointmentDto>> GetByDoctorAsync(int? doctorId);

    /// <summary>
    /// Gets records by date range
    /// </summary>
    /// <param name="startDate">Starting date</param>
    /// <param name="endDate">End date</param>
    /// <returns>List of DTO records</returns>
    public Task<List<AppointmentDto>> GetByDateRangeAsync(DateTime? startDate, DateTime? endDate);
}