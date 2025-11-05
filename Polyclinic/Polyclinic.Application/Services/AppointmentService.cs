using AutoMapper;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Interfaces;

namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для работы с записями на прием
/// </summary>
public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentManager _appointmentManager;
    private readonly IMapper _mapper;

    public AppointmentService(IAppointmentManager appointmentRepository, IMapper mapper)
    {
        _appointmentManager = appointmentRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<List<AppointmentDto>> GetAsync()
    {
        var appointments = await _appointmentManager.GetAllAsync();
        return _mapper.Map<List<AppointmentDto>>(appointments);
    }

    /// <inheritdoc/>
    public async Task<AppointmentDto?> GetAsync(int id)
    {
        var appointment = await _appointmentManager.GetByIdAsync(id);
        return _mapper.Map<AppointmentDto?>(appointment);
    }

    /// <inheritdoc/>
    public async Task<List<AppointmentDto>> GetByPatientAsync(int? patientId)
    {
        if (patientId == null)
            return new List<AppointmentDto>();

        var appointments = await _appointmentManager.GetByPatientAsync(patientId.Value);
        return _mapper.Map<List<AppointmentDto>>(appointments);
    }

    /// <inheritdoc/>
    public async Task<List<AppointmentDto>> GetByDoctorAsync(int? doctorId)
    {
        if (doctorId == null)
            return new List<AppointmentDto>();

        var appointments = await _appointmentManager.GetByDoctorAsync(doctorId.Value);
        return _mapper.Map<List<AppointmentDto>>(appointments);
    }

    /// <inheritdoc/>
    public async Task<List<AppointmentDto>> GetByDateRangeAsync(DateTime? startDate, DateTime? endDate)
    {
        if (startDate == null || endDate == null)
            return new List<AppointmentDto>();

        var appointments = await _appointmentManager.GetByDateRangeAsync(startDate.Value, endDate.Value);
        return _mapper.Map<List<AppointmentDto>>(appointments);
    }

    /// <inheritdoc/>
    public async Task<AppointmentDto> CreateAsync(AppointmentCreateUpdateDto dto)
    {
        var appointment = _mapper.Map<Appointment>(dto);
        var created = await _appointmentManager.CreateAsync(appointment);
        return _mapper.Map<AppointmentDto>(created);
    }

    /// <inheritdoc/>
    public async Task<AppointmentDto> UpdateAsync(int id, AppointmentCreateUpdateDto dto)
    {
        var appointment = _mapper.Map<Appointment>(dto);
        appointment.Id = id;
        var updated = await _appointmentManager.UpdateAsync(appointment);
        return _mapper.Map<AppointmentDto>(updated);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        await _appointmentRepository.DeleteAsync(id);
    }
}