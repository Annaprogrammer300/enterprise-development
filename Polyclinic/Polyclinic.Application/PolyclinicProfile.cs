using AutoMapper;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Appointments;

namespace Polyclinic.Application;

/// <summary>
/// Профиль AutoMapper для маппинга DTO
/// </summary>
public class PolyclinicProfile : Profile
{
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
            .ForMember(dest => dest.PatientFullName, opt => opt.MapFrom(src => src.Patient.FullName))
            .ForMember(dest => dest.DoctorFullName, opt => opt.MapFrom(src => src.Doctor.FullName))
            .ForMember(dest => dest.DoctorSpecialization, opt => opt.MapFrom(src => src.Doctor.Specialization.ToString()));
        CreateMap<AppointmentCreateUpdateDto, Appointment>();
    }
}