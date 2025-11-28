using AutoMapper;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Domain;

namespace Polyclinic.Application;

/// <summary>
/// AutoMapper profile for DTO mapping between domain entities and application contracts
/// </summary>
public class PolyclinicProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the PolyclinicProfile class and configures entity-DTO mappings
    /// </summary>
    public PolyclinicProfile()
    {
        // Patient mappings
        CreateMap<Patient, PatientDto>();
        CreateMap<PatientCreateUpdateDto, Patient>();

        // Doctor mappings
        CreateMap<Doctor, DoctorDto>();
        CreateMap<DoctorCreateUpdateDto, Doctor>();

        // Appointment mappings
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Patient.Id))
            .ForMember(dest => dest.PatientFullName, opt => opt.MapFrom(src => src.Patient.FullName))
            .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.Doctor.Id))
            .ForMember(dest => dest.DoctorFullName, opt => opt.MapFrom(src => src.Doctor.FullName))
            .ForMember(dest => dest.DoctorSpecialization, opt => opt.MapFrom(src => src.Doctor.Specialization.ToString()));
        CreateMap<AppointmentCreateUpdateDto, Appointment>()
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

    }
}