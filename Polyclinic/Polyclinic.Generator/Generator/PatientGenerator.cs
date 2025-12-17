using Bogus;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Domain;
using Polyclinic.Domain.Enum;

namespace Polyclinic.Generator.Generator;

/// <summary>
/// Patient Generator using Bogus
/// </summary>
public static class PatientGenerator
{
    /// <summary>
    /// Generates a list of Domain Entity patients
    /// </summary>
    /// <param name="count">Number of patients to generate</param>
    /// <returns>Domain Entity patient list</returns>
    public static List<Patient> GeneratePatients(int count)
    {
        var faker = new Faker<Patient>()
            .RuleFor(p => p.PassportNumber, f => $"{f.Random.Int(100000, 999999)}")
            .RuleFor(p => p.FullName, f => f.Name.FullName())
            .RuleFor(p => p.Gender, f => f.PickRandom<Gender>())
            .RuleFor(p => p.BirthDate, f => DateTime.SpecifyKind(
                f.Date.Past(60, DateTime.Now.AddYears(-18)),
                DateTimeKind.Utc))
            .RuleFor(p => p.Address, f => f.Address.FullAddress())
            .RuleFor(p => p.BloodGroup, f => f.PickRandom<BloodGroup>())
            .RuleFor(p => p.RhesusFactor, f => f.PickRandom<RhesusFactor>())
            .RuleFor(p => p.Phone, f => f.Phone.PhoneNumber());

        return faker.Generate(count);
    }

    /// <summary>
    /// Converts the Domain Entity Patient to a PatientDto for the API
    /// The ID is taken from the database after saving
    /// </summary>
    public static PatientDto ToPatientDto(Patient patient)
    {
        return new PatientDto
        {
            Id = patient.Id,
            PassportNumber = patient.PassportNumber,
            FullName = patient.FullName,
            Gender = patient.Gender.ToString(),
            BirthDate = patient.BirthDate,
            Address = patient.Address,
            BloodGroup = patient.BloodGroup.ToString(),
            RhesusFactor = patient.RhesusFactor.ToString(),
            Phone = patient.Phone
        };
    }
}