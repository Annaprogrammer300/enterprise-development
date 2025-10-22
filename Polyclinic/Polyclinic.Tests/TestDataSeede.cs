using Polyclinic.Components;
using Polyclinic.Components.Enum;

namespace Polyclinic.Tests;

/// <summary>
/// Класс для генерации тестовых данных (пациенты, врачи, приёмы).
/// Используется в unit-тестах.
/// </summary>
public static class TestDataSeeder
{
    /// <summary>
    /// Возвращает список тестовых пациентов.
    /// </summary>
    public static List<Patient> GetPatients() => new()
    {
            new Patient { PassportNumber="P001", FullName="Иванов Иван", Gender=Gender.Male, BirthDate=new DateTime(1990,5,12), 
                Address="ул. Ленина, 1", BloodGroup=BloodGroup.I, RhesusFactor=RhesusFactor.Positive, Phone="111-111" },
            new Patient { PassportNumber="P002", FullName="Петров Петр", Gender=Gender.Male, BirthDate=new DateTime(1985,8,3), 
                Address="ул. Гагарина, 2", BloodGroup=BloodGroup.II, RhesusFactor=RhesusFactor.Negative, Phone="222-222" },
            new Patient { PassportNumber="P003", FullName="Сидорова Анна", Gender=Gender.Female, BirthDate=new DateTime(1995,10,1), 
                Address="ул. Молодежная, 5", BloodGroup=BloodGroup.III, RhesusFactor=RhesusFactor.Positive, Phone="333-333" },
            new Patient { PassportNumber="P004", FullName="Кузнецов Сергей", Gender=Gender.Male, BirthDate=new DateTime(2000,3,22), 
                Address="ул. Советская, 9", BloodGroup=BloodGroup.I, RhesusFactor=RhesusFactor.Negative, Phone="444-444" },
            new Patient { PassportNumber="P005", FullName="Смирнова Мария", Gender=Gender.Female, BirthDate=new DateTime(1978,7,19), 
                Address="ул. Победы, 10", BloodGroup=BloodGroup.IV, RhesusFactor=RhesusFactor.Positive, Phone="555-555" },
            new Patient { PassportNumber="P006", FullName="Орлов Дмитрий", Gender=Gender.Male, BirthDate=new DateTime(1992,11,11), 
                Address="ул. Центральная, 12", BloodGroup=BloodGroup.II, RhesusFactor=RhesusFactor.Positive, Phone="666-666" },
            new Patient { PassportNumber="P007", FullName="Васильева Ирина", Gender=Gender.Female, BirthDate=new DateTime(1988,2,9), 
                Address="ул. Чехова, 3", BloodGroup=BloodGroup.III, RhesusFactor=RhesusFactor.Negative, Phone="777-777" },
            new Patient { PassportNumber="P008", FullName="Морозов Алексей", Gender=Gender.Male, BirthDate=new DateTime(1999,1,15), 
                Address="ул. Спортивная, 4", BloodGroup=BloodGroup.I, RhesusFactor=RhesusFactor.Positive, Phone="888-888" },
            new Patient { PassportNumber="P009", FullName="Николаева Елена", Gender=Gender.Female, BirthDate=new DateTime(1980,4,4), 
                Address="ул. Пушкина, 8", BloodGroup=BloodGroup.IV, RhesusFactor=RhesusFactor.Negative, Phone="999-999" },
            new Patient { PassportNumber="P010", FullName="Зайцев Павел", Gender=Gender.Male, BirthDate=new DateTime(1993,6,6), 
                Address="ул. Луговая, 6", BloodGroup=BloodGroup.II, RhesusFactor=RhesusFactor.Positive, Phone="101-010" }
        };

    /// <summary>
    /// Возвращает список тестовых врачей.
    /// </summary>
    public static List<Doctor> GetDoctors() => new()
    {
            new Doctor { PassportNumber="D001", FullName="Доктор Хаус", BirthYear=1970, Specialization=Specialization.Therapist, Experience=25 },
            new Doctor { PassportNumber="D002", FullName="Айболит Иван", BirthYear=1980, Specialization=Specialization.Pediatrician, Experience=15 },
            new Doctor { PassportNumber="D003", FullName="Сергеев Павел", BirthYear=1990, Specialization=Specialization.Cardiologist, Experience=8 },
            new Doctor { PassportNumber="D004", FullName="Михайлова Ольга", BirthYear=1985, Specialization=Specialization.Dentist, Experience=12 },
            new Doctor { PassportNumber="D005", FullName="Сидоров Алексей", BirthYear=1975, Specialization=Specialization.Surgeon, Experience=20 },
            new Doctor { PassportNumber="D006", FullName="Кузнецова Марина", BirthYear=1992, Specialization=Specialization.Neurologist, Experience=5 },
            new Doctor { PassportNumber="D007", FullName="Громов Олег", BirthYear=1983, Specialization=Specialization.Orthopedist, Experience=14 },
            new Doctor { PassportNumber="D008", FullName="Иванова Татьяна", BirthYear=1987, Specialization=Specialization.Dermatologist, Experience=11 },
            new Doctor { PassportNumber="D009", FullName="Павлова Светлана", BirthYear=1991, Specialization=Specialization.Ophthalmologist, Experience=9 },
            new Doctor { PassportNumber="D010", FullName="Белый Роман", BirthYear=1984, Specialization=Specialization.Psychiatrist, Experience=16 }
        };

    /// <summary>
    /// Возвращает список тестовых записей на приём.
    /// </summary>
    public static List<Appointment> GetAppointments(List<Patient> patients, List<Doctor> doctors)
    {
        var now = DateTime.Now;
        return new List<Appointment>
            {
                new () { Patient=patients[0], Doctor=doctors[0], DateTime=new DateTime(2025, 8, 15, 14, 50,0), RoomNumber=101, IsRepeat=false },
                new () { Patient=patients[1], Doctor=doctors[1], DateTime=new DateTime(2025, 6, 30, 12, 30,0), RoomNumber=102, IsRepeat=true },
                new () { Patient=patients[2], Doctor=doctors[2], DateTime=new DateTime(2025, 10, 30, 12, 00,0), RoomNumber=103, IsRepeat=false },
                new () { Patient=patients[3], Doctor=doctors[3], DateTime=new DateTime(2025, 6, 7, 11, 30,0), RoomNumber=104, IsRepeat=true },
                new () { Patient=patients[4], Doctor=doctors[4], DateTime=new DateTime(2025, 6, 10, 12, 20,0), RoomNumber=105, IsRepeat=true },
                new () { Patient=patients[5], Doctor=doctors[4], DateTime=new DateTime(2025, 7, 24, 10, 00,0), RoomNumber=105, IsRepeat=false },
                new () { Patient=patients[5], Doctor=doctors[0], DateTime=new DateTime(2025, 6, 1, 12, 30,0), RoomNumber=101, IsRepeat=true },
                new () { Patient=patients[6], Doctor=doctors[2], DateTime=new DateTime(2025, 3, 30, 14, 30,0), RoomNumber=103, IsRepeat=false },
                new () { Patient=patients[7], Doctor=doctors[7], DateTime=new DateTime(2025, 6, 17, 12, 00,0), RoomNumber=108, IsRepeat=true },
                new () { Patient=patients[8], Doctor=doctors[8], DateTime=new DateTime(2025, 8, 8, 16, 30,0), RoomNumber=108, IsRepeat=false },
                new () { Patient=patients[9], Doctor=doctors[9], DateTime=new DateTime(2025, 9, 4, 12, 24,0), RoomNumber=109, IsRepeat=true }
            };
    }
}
