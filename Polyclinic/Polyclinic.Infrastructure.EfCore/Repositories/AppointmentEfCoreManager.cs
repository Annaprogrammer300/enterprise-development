using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

public class AppointmentEfCoreManager : IManager<Appointment, int>
{
    private readonly PolyclinicDbContext _db;
    private readonly DbSet<Appointment> _appointments;

    public AppointmentEfCoreManager(PolyclinicDbContext dbContext)
    {
        _db = dbContext;
        _appointments = _db.Set<Appointment>();
    }

    public Appointment Create(Appointment entity)
    {
        _appointments.Add(entity);
        _db.SaveChanges();
        return entity;
    }

    public Appointment? Read(int entityId) =>
        _appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefault(a => a.Id == entityId);

    public List<Appointment> ReadAll() =>
        [.. _appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .OrderBy(a => a.Id)];

    public void Update(Appointment entity)
    {
        _appointments.Update(entity);
        _db.SaveChanges();
    }

    public void Delete(int entityId)
    {
        var item = Read(entityId);
        if (item != null)
        {
            _appointments.Remove(item);
            _db.SaveChanges();
        }
    }

    public bool Exists(int entityId) =>
        _appointments.Any(a => a.Id == entityId);
}
