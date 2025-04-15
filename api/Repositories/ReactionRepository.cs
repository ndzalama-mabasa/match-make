using System.Data;
using Dapper;
using galaxy_match_make.Models;

namespace galaxy_match_make.Repositories;

public class ReactionRepository : IReactionRepository
{
    private readonly IDbConnection _dbConnection;

    public ReactionRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public async Task<List<ReactionDto>> GetReactions(Guid userId)
    {
        const string query = @"
        SELECT 
            p.user_id,
            p.display_name,
            p.avatar_url
        FROM reactions r1
        JOIN reactions r2 
            ON r1.target_id = r2.reactor_id 
           AND r1.reactor_id = r2.target_id
           AND r2.is_positive = true
        JOIN profiles p ON r1.target_id = p.user_id
        WHERE r1.reactor_id = @UserId
          AND r1.is_positive = true";

        var results = await _dbConnection.QueryAsync<ReactionDto>(query, new { UserId = userId });
        return results.ToList();
    }

    
    public async Task<List<ReactionDto>> GetSentRequests(Guid userId)
    {
        const string query = @"
        SELECT 
            p.user_id,
            p.display_name,
            p.avatar_url
        FROM reactions r1
        JOIN profiles p ON r1.target_id = p.user_id
        WHERE r1.reactor_id = @UserId
          AND r1.is_positive = true
          AND NOT EXISTS (
              SELECT 1 
              FROM reactions r2 
              WHERE r2.reactor_id = r1.target_id
                AND r2.target_id = r1.reactor_id
                AND r2.is_positive = true
          )";

        var results = await _dbConnection.QueryAsync<ReactionDto>(query, new { UserId = userId });
        return results.ToList();
    }

    public async Task<List<ReactionDto>> GetReceivedRequests(Guid userId)
    {
        const string query = @"
        SELECT 
            p.user_id,
            p.display_name,
            p.avatar_url
        FROM reactions r1
        JOIN profiles p ON r1.reactor_id = p.user_id
        WHERE r1.target_id = @UserId
          AND r1.is_positive = true
          AND NOT EXISTS (
              SELECT 1 
              FROM reactions r2 
              WHERE r2.reactor_id = r1.target_id
                AND r2.target_id = r1.reactor_id
                AND r2.is_positive = true
          )";

        var results = await _dbConnection.QueryAsync<ReactionDto>(query, new { UserId = userId });
        return results.ToList();
    }


}