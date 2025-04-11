using Dapper;
using galaxy_match_make.Models;
using Npgsql;

namespace galaxy_match_make.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_configuration
                .GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            using var connection = GetConnection();
            var users = await connection.QueryAsync<UserDto>("SELECT * FROM USERS");
            return users.ToList();
        }

        async Task<UserDto> IUserRepository.GetUserById(Guid id)
        {
            using var connection = GetConnection();
            connection.Open();
            var user = await connection
                .QueryFirstOrDefaultAsync<UserDto>("SELECT * FROM USERS WHERE ID= @Id", new { Id = id });

            return user;
        }

        Task IUserRepository.AddUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        Task IUserRepository.DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        Task IUserRepository.UpdateUser(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
