namespace Polyclinic.Application.Contracts.Appointments;

/// <summary>
/// Наследник аппликейшен службы для записей на прием
/// </summary>
public interface IAppointmentService : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    /// <summary>
    /// Получает записи по пациенту
    /// </summary>
    /// <param name="patientId">Идентификатор пациента</param>
    /// <returns>Список DTO записей</returns>
    public Task<List<AppointmentDto>> GetByPatientAsync(int? patientId);

    /// <summary>
    /// Получает записи по врачу
    /// </summary>
    /// <param name="doctorId">Идентификатор врача</param>
    /// <returns>Список DTO записей</returns>
    public Task<List<AppointmentDto>> GetByDoctorAsync(int? doctorId);

    /// <summary>
    /// Получает записи по диапазону дат
    /// </summary>
    /// <param name="startDate">Начальная дата</param>
    /// <param name="endDate">Конечная дата</param>
    /// <returns>Список DTO записей</returns>
    public Task<List<AppointmentDto>> GetByDateRangeAsync(DateTime? startDate, DateTime? endDate);
}