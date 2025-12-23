using Bogus;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Domain.Enum;

namespace Polyclinic.Generator.Generator;

/// <summary>
/// Static class that generates randomized patient DTO records
/// Produces a collection of <see cref="PatientCreateUpdateDto"/> 
/// instances with personal, contact, and medical data in UTC for date fields.
/// </summary>
public static class PatientGenerator
{
    public static List<PatientCreateUpdateDto> GeneratePatientDtos(int count)
    {
        var faker = new Faker<PatientCreateUpdateDto>()
            .CustomInstantiator(f => new PatientCreateUpdateDto(
                PassportNumber: $"{f.Random.Int(100000, 999999)}",
                FullName: f.Name.FullName(),
                Gender: f.PickRandom<Gender>().ToString(),
                BirthDate: DateTime.SpecifyKind(
                    f.Date.Past(60, DateTime.Now.AddYears(-18)),
                    DateTimeKind.Utc),
                Address: f.Address.FullAddress(),
                BloodGroup: f.PickRandom<BloodGroup>().ToString(),
                RhesusFactor: f.PickRandom<RhesusFactor>().ToString(),
                Phone: f.Phone.PhoneNumber()
            ));

        return faker.Generate(count);
    }
}