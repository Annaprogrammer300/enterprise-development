using Polyclinic.Enum;

namespace Polyclinic.Application.Contracts.Doctors;

/// <summary>
/// Heir to the medical application service
/// </summary>
public interface IDoctorService : IApplicationService<DoctorDto, DoctorCreateUpdateDto, int>
{
    /// <summary>
    /// Gets doctors by specialization
    /// </summary>
    /// <param name="specialization">Specialization</param>
    /// <returns>List of DTO doctors</returns>
    public Task<List<DoctorDto>> GetBySpecializationAsync(Specialization? specialization);

    /// <summary>
    /// Receives doctors with minimal experience
    /// </summary>
    /// <param name="minExperience">Minimal experience</param>
    /// <returns>List of DTO doctors</returns>
    public Task<List<DoctorDto>> GetWithMinExperienceAsync(int? minExperience);
}