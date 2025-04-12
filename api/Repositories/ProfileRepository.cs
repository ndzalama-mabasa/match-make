using System.Numerics;
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
        var profiles = connection.Query<ProfileDto, SpeciesDto, PlanetDto, GenderDto, UserInterestsDto, ProfileDto>(
                sql,
                (profile, species, planet, gender, interest) =>
                {
                    // Initialize UserInterests if it's null
                    if (profile.UserInterests == null)
                        profile.UserInterests = new List<UserInterestsDto>();

                    // Add interest to the profile's UserInterests list
                    if (interest != null)
                    {
                        profile.UserInterests.Add(new UserInterestsDto
                        {
                            InterestId = interest.InterestId,
                            InterestName = interest.InterestName
                        });
                    }

                    // Check if the profile already exists in the dictionary
                    if (!profileDictionary.ContainsKey(profile.Id))
                    {
                        // If not, add the profile
                        profile.Species = species;
                        profile.Planet = planet;
                        profile.Gender = gender;
                        profileDictionary.Add(profile.Id, profile);
                    }
                    else
                    {
                        // If the profile exists, just add new interests
                        var existingProfile = profileDictionary[profile.Id];

                        // Add the interests only if they're not already in the list
                        foreach (var userInterest in profile.UserInterests)
                        {
                            if (!existingProfile.UserInterests.Any(ui => ui.InterestId == userInterest.InterestId))
                            {
                                existingProfile.UserInterests.Add(userInterest);
                            }
                        }
                    }
                    return profile;
                },
                parameters,
                splitOn: "species_id, planet_id, gender_id, interest_id"
            );

        return profileDictionary.Values;
    }

    private string GetProfileSql(bool withWhereClause)
    {
        var sql = @"
        SELECT 
            p.id, p.user_id, p.display_name, p.bio, p.avatar_url, 
            p.height_in_galactic_inches, p.galactic_date_of_birth,
            s.id AS species_id, s.species_name,
            pl.id AS planet_id, pl.planet_name,
            g.id AS gender_id, g.gender,
            ui.user_id AS user_interest_user_id, 
            i.id AS interest_id, i.interest_name
        FROM profiles p
        LEFT JOIN species s ON p.species_id = s.id
        LEFT JOIN planets pl ON p.planet_id = pl.id
        LEFT JOIN genders g ON p.gender_id = g.id
        LEFT JOIN user_interests ui ON p.user_id = ui.user_id
        LEFT JOIN interests i ON ui.interest_id = i.id";

        if (withWhereClause) sql += " WHERE p.user_id=@Id;";

        return sql;


    }

}
