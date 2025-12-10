using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

public class DoctorEfCoreManager : IManager<Doctor, int>
{
    private readonly PolyclinicDbContext _db;
    private readonly DbSet<Doctor> _doctors;

    public DoctorEfCoreManager(PolyclinicDbContext dbContext)
    {
        _db = dbContext;
        _doctors = _db.Set<Doctor>();
    }

    public void Create(Doctor entity)
    {
        _doctors.Add(entity);
        _db.SaveChanges();
    }

    public Doctor? Read(int entityId) =>
        _doctors.FirstOrDefault(d => d.Id == entityId);

    public List<Doctor> ReadAll() =>
        _doctors.ToList();

    public void Update(Doctor entity)
    {
        _doctors.Update(entity);
        _db.SaveChanges();
    }

    public void Delete(int entityId)
    {
        var item = Read(entityId);
        if (item != null)
        {
            _doctors.Remove(item);
            _db.SaveChanges();
        }
    }

    public bool Exists(int entityId) =>
        _doctors.Any(d => d.Id == entityId);
}
