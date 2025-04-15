using galaxy_match_make.Models;

namespace galaxy_match_make.Repositories;

public interface IReactionRepository
{
    Task<List<ReactionDto>> GetReactions(Guid userId);
    Task<List<ReactionDto>> GetSentRequests(Guid userId);
    Task<List<ReactionDto>> GetReceivedRequests(Guid userId);
}