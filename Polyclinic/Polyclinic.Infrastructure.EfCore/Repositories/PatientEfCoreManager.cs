using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain;
using Polyclinic.Domain.Abstractions;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

public class PatientEfCoreManager : IManager<Patient, int>
{
    private readonly PolyclinicDbContext _db;
    private readonly DbSet<Patient> _patients;

    public PatientEfCoreManager(PolyclinicDbContext dbContext)
    {
        _db = dbContext;
        _patients = _db.Set<Patient>();
    }

    public void Create(Patient entity)
    {
        _patients.Add(entity);
        _db.SaveChanges();
    }

    public Patient? Read(int entityId) =>
        _patients.FirstOrDefault(p => p.Id == entityId);

    public List<Patient> ReadAll() =>
        [.. _patients];

    public void Update(Patient entity)
    {
        _patients.Update(entity);
        _db.SaveChanges();
    }

    public void Delete(int entityId)
    {
        var item = Read(entityId);
        if (item != null)
        {
            _patients.Remove(item);
            _db.SaveChanges();
        }
    }

    public bool Exists(int entityId) =>
        _patients.Any(p => p.Id == entityId);
}
