using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain;

namespace Polyclinic.Infrastructure.EfCore;

public class PolyclinicDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Doctor
        modelBuilder.Entity<Doctor>(builder =>
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.PassportNumber).IsRequired();
            builder.Property(d => d.FullName).IsRequired();

            // сидирование докторов
            builder.HasData(DataSeeder.GetDoctors());
        });

        // Patient
        modelBuilder.Entity<Patient>(builder =>
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PassportNumber).IsRequired();
            builder.Property(p => p.FullName).IsRequired();
            builder.Property(p => p.Address).IsRequired();
            builder.Property(p => p.Phone).IsRequired();

            // сидирование пациентов
            builder.HasData(DataSeeder.GetPatients());
        });

        // Appointment
        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Patient)
                .WithMany() // у Patient нет коллекции Appointments
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor)
                .WithMany() // у Doctor нет коллекции Appointments
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.DateTime).IsRequired();
            builder.Property(a => a.RoomNumber).IsRequired();

            // сидирование приёмов: из DataSeeder берём Id пациента и доктора
            var appointmentsSeed = DataSeeder.GetAppointments()
                .Select(a => new
                {
                    a.Id,
                    a.DateTime,
                    a.RoomNumber,
                    a.IsRepeat,
                    PatientId = a.Patient.Id,
                    DoctorId = a.Doctor.Id
                });

            builder.HasData(appointmentsSeed);
        });
    }
}
