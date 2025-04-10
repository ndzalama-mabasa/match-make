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

    public async Task<IEnumerable<ProfileDto>> GetAllProfiles()
    {
        var sql =GetProfileSql(false);
        var profiles = await QueryProfiles(sql, false);
        return profiles;
    }

    public async Task<ProfileDto> GetProfileById(Guid id)
    {
        var sql = GetProfileSql(true);
        var profile = await QueryProfiles(sql, new { Id = id});

        return profile.FirstOrDefault();
    }


    private async Task<IEnumerable<ProfileDto>> QueryProfiles(string sql, object? parameters = null)
    {
        var profileDictionary = new Dictionary<int, ProfileDto>();
        using var connection = GetConnection();
        var profiles = await connection.QueryAsync<ProfileDto, SpeciesDto, PlanetDto, GenderDto, ProfileDto>(
            sql,
            (profile, species, planet, gender) =>
            {
                profile.Species = species;
                profile.Planet = planet;
                profile.Gender = gender;
                profileDictionary.Add(profile.Id, profile);
                return profile;
            },
            parameters,
            splitOn: "id,id,id"
        );

        return profileDictionary.Values;
    }

    private string GetProfileSql(bool withWhereClause)
    {
        var sql = @"
        SELECT 
            p.id, p.user_id, p.display_name, p.bio, p.avatar_url, 
            p.height_in_galactic_inches, p.galactic_date_of_birth,

            s.id, s.species_name,
            pl.id, pl.planet_name,
            g.id, g.gender
        FROM profiles p
        LEFT JOIN species s ON p.species_id = s.id
        LEFT JOIN planets pl ON p.planet_id = pl.id
        LEFT JOIN genders g ON p.gender_id = g.id";

        if (withWhereClause) sql += " WHERE p.user_id=@Id";

        return sql;


    }

}
