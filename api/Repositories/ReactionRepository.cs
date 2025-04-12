using Dapper;
using galaxy_match_make.Models;
using System.Data;

namespace galaxy_match_make.Repositories;

public class ReactionRepository : IReactionRepository
{
    private readonly IDbConnection _dbConnection;

    public ReactionRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<ReactionDto>> GetAllReactionsAsync()
    {
        const string query = "SELECT id, reactor_id, target_id, is_positive FROM reactions";
        return await _dbConnection.QueryAsync<ReactionDto>(query);
    }

    public async Task<ReactionDto?> GetReactionByIdAsync(int id)
    {
        const string query = "SELECT id, reactor_id, target_id, is_positive FROM reactions WHERE id = @Id";
        return await _dbConnection.QuerySingleOrDefaultAsync<ReactionDto>(query, new { Id = id });
    }

    public async Task AddReactionAsync(ReactionDto reaction)
    {
        const string query = "INSERT INTO reactions (reactor_id, target_id, is_positive) VALUES (@ReactorId, @TargetId, @IsPositive)";
        await _dbConnection.ExecuteAsync(query, reaction);
    }

    public async Task UpdateReactionAsync(ReactionDto reaction)
    {
        const string query = "UPDATE reactions SET reactor_id = @ReactorId, target_id = @TargetId, is_positive = @IsPositive WHERE id = @Id";
        await _dbConnection.ExecuteAsync(query, reaction);
    }

    public async Task DeleteReactionAsync(int id)
    {
        const string query = "DELETE FROM reactions WHERE id = @Id";
        await _dbConnection.ExecuteAsync(query, new { Id = id });
    }

    public async Task<IEnumerable<ReactionDto>> GetReactionsByReactorAsync(Guid userId)
    {
        const string query = "SELECT * FROM reactions WHERE reactor_id = @UserId";
        return await _dbConnection.QueryAsync<ReactionDto>(query, new { UserId = userId });
    }

    public async Task<ReactionDto?> GetReactionByReactorAndTargetAsync(Guid reactorId, Guid targetId)
    {
        const string query = "SELECT * FROM reactions WHERE reactor_id = @ReactorId AND target_id = @TargetId";
        return await _dbConnection.QuerySingleOrDefaultAsync<ReactionDto>(query, new { ReactorId = reactorId, TargetId = targetId });
    }
}