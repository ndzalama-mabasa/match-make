using System.Numerics;
using System.Text.Json;
using Dapper;
using galaxy_match_make.Data;
using galaxy_match_make.Models;
using Humanizer;
using Npgsql;
using NuGet.Protocol.Plugins;

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
        var profiles = connection.Query<ProfileDto, SpeciesDto, PlanetDto, GenderDto, string, ProfileDto>(
                sql,
                (profile, species, planet, gender, userInterestsJson) =>
                {
                    profile.Species = species;
                    profile.Planet = planet;
                    profile.Gender = gender;

                    if (!string.IsNullOrEmpty(userInterestsJson))
                    {
                        profile.UserInterests = JsonSerializer.Deserialize<List<UserInterestsDto>>(userInterestsJson);
                    }
                    else
                    {
                        profile.UserInterests = new List<UserInterestsDto>();
                    }

                    profileDictionary[profile.Id] = profile;

                    return profile;
                },
                parameters,
                splitOn: "id, id, id, user_interests"
                );

        return profileDictionary.Values;
    }

    private string GetProfileSql(bool withWhereClause)
    {
        var sql = @"
            SELECT 
                p.id,
                p.user_id,
                p.display_name,
                p.bio,
                p.avatar_url,
                p.height_in_galactic_inches,
                p.galactic_date_of_birth,
    
                s.id,
                s.species_name,
    
                pl.id,
                pl.planet_name,
    
                g.id,
                g.gender,
    
                -- Aggregated interests
                json_agg(
                    json_build_object(
                        'InterestId', i.id,
                        'InterestName', i.interest_name
                    )
                ) AS user_interests

            FROM profiles p
            LEFT JOIN species s ON p.species_id = s.id
            LEFT JOIN planets pl ON p.planet_id = pl.id
            LEFT JOIN genders g ON p.gender_id = g.id
            LEFT JOIN user_interests ui ON p.user_id = ui.user_id
            LEFT JOIN interests i ON ui.interest_id = i.id";

            if (withWhereClause)
            {
                sql += " WHERE p.user_id = @Id ";
            }

            sql += @"
                GROUP BY 
                    p.id, p.user_id, p.display_name, p.bio, p.avatar_url,
                    p.height_in_galactic_inches, p.galactic_date_of_birth,
                    s.id, s.species_name,
                    pl.id, pl.planet_name,
                    g.id, g.gender;";
        return sql;


    }

    Task IProfileRepository.UpdateProfile(ProfileDto profile)
    {
        throw new NotImplementedException();
    }

    Task IProfileRepository.CreateProfile(ProfileDto profile)
    {
        throw new NotImplementedException();
    }
}
