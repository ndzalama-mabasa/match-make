using galaxy_match_make.Models;

namespace galaxy_match_make.Repositories
{
    public interface IProfileRepository
    {
        Task<IEnumerable<ProfileDto>> GetAllProfiles();
        Task<ProfileDto> GetProfileById(Guid id);

        Task UpdateProfile(ProfileDto profile);
        Task CreateProfile(ProfileDto profile);
    }
}
