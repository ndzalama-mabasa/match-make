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

    public async Task<ProfileDto> UpdateProfile(Guid id, UpdateProfileDto profile)
    {
        var sql = GetUpsertProfileSql(true);

        using var connection = GetConnection();
        await connection.OpenAsync(); 
        using var transaction = await connection.BeginTransactionAsync();

        try
        {
            var updatedProfileId = await connection.ExecuteScalarAsync<int>(sql, new
            {
                UserId = id,
                DisplayName = profile.DisplayName,
                Bio = profile.Bio,
                AvatarUrl = profile.AvatarUrl,
                SpeciesId = profile.SpeciesId,
                PlanetId = profile.PlanetId,
                GenderId = profile.GenderId,
                HeightInGalacticInches = profile.HeightInGalacticInches,
                GalacticDateOfBirth = profile.GalacticDateOfBirth
            }, transaction);

            var deleteInterestsSql = "DELETE FROM user_interests WHERE user_id = @UserId;";
            await connection.ExecuteAsync(deleteInterestsSql, new { UserId = id }, transaction);

            if (profile.UserInterestIds != null && profile.UserInterestIds.Any())
            {
                var insertInterestsSql = @"
                    INSERT INTO user_interests (user_id, interest_id)
                    VALUES (@UserId, @InterestId);";

                foreach (var interestId in profile.UserInterestIds)
                {
                    await connection.ExecuteAsync(insertInterestsSql, new
                    {
                        UserId = id,
                        InterestId = interestId
                    }, transaction);
                }
            }

            await transaction.CommitAsync();
            return await GetProfileById(id); 
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<ProfileDto> CreateProfile(CreateProfileDto profile)
    {
        var sql = GetUpsertProfileSql(false);

        using var connection = GetConnection();
        await connection.OpenAsync(); 
        using var transaction = await connection.BeginTransactionAsync(); 

        try
        {
            var profileId = await connection.ExecuteScalarAsync<int>(sql, new
            {
                UserId = profile.UserId,
                DisplayName = profile.DisplayName,
                Bio = profile.Bio,
                AvatarUrl = profile.AvatarUrl,
                SpeciesId = profile.SpeciesId,
                PlanetId = profile.PlanetId,
                GenderId = profile.GenderId,
                HeightInGalacticInches = profile.HeightInGalacticInches,
                GalacticDateOfBirth = profile.GalacticDateOfBirth
            }, transaction);

            if (profile.UserInterestIds != null && profile.UserInterestIds.Any())
            {
                var insertInterestsSql = @"
                    INSERT INTO user_interests (user_id, interest_id)
                    VALUES (@UserId, @InterestId);";

                foreach (var interestId in profile.UserInterestIds)
                {
                    await connection.ExecuteAsync(insertInterestsSql, new
                    {
                        UserId = profile.UserId,
                        InterestId = interestId
                    }, transaction);
                }
            }

            await transaction.CommitAsync();
            return await GetProfileById(profile.UserId);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
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

        return sql;
    }

    public string GetUpsertProfileSql(bool isUpdate)
    {
        return isUpdate ? @"
            UPDATE profiles 
            SET display_name = @DisplayName, 
                bio = @Bio, 
                avatar_url = @AvatarUrl, 
                species_id = @SpeciesId, 
                planet_id = @PlanetId, 
                gender_id = @GenderId, 
                height_in_galactic_inches = @HeightInGalacticInches, 
                galactic_date_of_birth = @GalacticDateOfBirth
            WHERE user_id = @UserId
            RETURNING id;" : @"
            INSERT INTO profiles 
            (user_id, display_name, bio, avatar_url, species_id, planet_id, gender_id, height_in_galactic_inches, galactic_date_of_birth)
            VALUES 
            (@UserId, @DisplayName, @Bio, @AvatarUrl, @SpeciesId, @PlanetId, @GenderId, @HeightInGalacticInches, @GalacticDateOfBirth)
            RETURNING id;";
    }

}
