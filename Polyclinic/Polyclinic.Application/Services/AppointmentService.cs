using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Infrastructure.InMemory;

namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для CRUD-операций над записями на прием
/// </summary>
/// <param name="manager">Менеджер записей</param>
/// <param name="mapper">Профиль маппинга</param>
public class AppointmentService(IManager<Appointment, int> manager, IMapper mapper) : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public AppointmentDto Create(AppointmentCreateUpdateDto dto)
    {
        var newAppointment = mapper.Map<Appointment>(dto);
        var lastId = manager.ReadAll().Count > 0 ? manager.ReadAll().Max(a => a.Id) : 0;
        newAppointment.Id = lastId + 1;
        manager.Create(newAppointment);
        return mapper.Map<AppointmentDto>(newAppointment);
    }

    /// <inheritdoc/>
    public void Delete(int dtoId)
    {
        manager.Delete(dtoId);
    }

    /// <inheritdoc/>
    public AppointmentDto Get(int dtoId)
    {
        var appointment = manager.Read(dtoId);
        return mapper.Map<AppointmentDto>(appointment);
    }

    /// <inheritdoc/>
    public List<AppointmentDto> GetAll()
    {
        var appointments = manager.ReadAll();
        return mapper.Map<List<AppointmentDto>>(appointments);
    }

    /// <inheritdoc/>
    public AppointmentDto Update(int dtoId, AppointmentCreateUpdateDto dto)
    {
        _ = manager.Read(dtoId) ?? throw new ArgumentException("Appointment not found");
        var updatedAppointment = mapper.Map<Appointment>(dto);
        updatedAppointment.Id = dtoId;
        manager.Update(updatedAppointment);
        return mapper.Map<AppointmentDto>(updatedAppointment);
    }
}