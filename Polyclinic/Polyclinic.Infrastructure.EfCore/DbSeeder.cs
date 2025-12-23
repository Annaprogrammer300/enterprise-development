using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain;

namespace Polyclinic.Infrastructure.EfCore;

/// <summary>
/// Provides methods for seeding the database with initial test data.
/// Contains separate methods for seeding each entity type and a combined method for all data.
/// </summary>
public static class DbSeeder
{
    /// <summary>
    /// Seeds the database with patients if the table is empty.
    /// Also resets the auto-increment counter to continue from the next available ID.
    /// </summary>
    public static async Task SeedPatientsAsync(PolyclinicDbContext context)
    {
        if (!await context.Patients.AnyAsync())
        {
            var patients = DataSeeder.GetPatients();

            foreach (var p in patients)
            {
                if (p.BirthDate.Kind == DateTimeKind.Unspecified)
                {
                    // считаем, что дата в локном времени и приводим к UTC
                    p.BirthDate = DateTime.SpecifyKind(p.BirthDate, DateTimeKind.Utc);
                }
            }

            context.Patients.AddRange(patients);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Seeds the database with doctors if the table is empty.
    /// Also resets the auto-increment counter to continue from the next available ID.
    /// </summary>
    public static async Task SeedDoctorsAsync(PolyclinicDbContext context)
    {
        if (!await context.Doctors.AnyAsync())
        {
            context.Doctors.AddRange(DataSeeder.GetDoctors());
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Seeds the database with appointments if the table is empty.
    /// Also resets the auto-increment counter to continue from the next available ID.
    /// </summary>
    public static async Task SeedAppointmentsAsync(PolyclinicDbContext context)
    {
        if (!await context.Appointments.AnyAsync())
        {
            var appointments = DataSeeder.GetAppointments();

            foreach (var a in appointments)
            {
                if (a.DateTime.Kind == DateTimeKind.Unspecified)
                {
                    a.DateTime = DateTime.SpecifyKind(a.DateTime, DateTimeKind.Utc);
                }

                // если есть ещё поля DateTime
                // if (a.StartTime.Kind == DateTimeKind.Unspecified)
                //     a.StartTime = DateTime.SpecifyKind(a.StartTime, DateTimeKind.Utc);
            }

            context.Appointments.AddRange(appointments);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Performs complete database seeding in the correct order to maintain referential integrity.
    /// Order: Patients → Doctors → Appointments.
    /// </summary>
    /// <param name="context">The database context to seed data into.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task SeedAllAsync(PolyclinicDbContext context)
    {
        await SeedPatientsAsync(context);
        await SeedDoctorsAsync(context);
        await SeedAppointmentsAsync(context);
    }
}
