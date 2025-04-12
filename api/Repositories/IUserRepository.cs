using galaxy_match_make.Models;

namespace galaxy_match_make.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(Guid id);
        Task UpdateUser(UserDto user);
        Task DeleteUser(Guid id);
        
        Task AddUser(string oauthId);
        
        Task<UserDto> GetUserByOauthId(string oauthId);

    }
}
