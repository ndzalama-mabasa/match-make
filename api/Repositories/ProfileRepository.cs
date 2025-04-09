using Dapper;
using galaxy_match_make.Data;
using galaxy_match_make.Models;
using Npgsql;

namespace galaxy_match_make.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly IConfiguration _configuration;

    public ProfileRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    private NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_configuration
            .GetConnectionString("DefaultConnection"));
    }

    public async Task<List<ProfileDto>> GetAllProfiles()
    {
        using var connection = GetConnection();
        var profiles = await connection.QueryAsync<ProfileDto>("SELECT * FROM PROFILES");
        return profiles.ToList();
    }
}
