namespace galaxy_match_make.Controllers;

using System.Security.Claims;
using galaxy_match_make.Models;
using galaxy_match_make.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReactionsController : ControllerBase
{
    private readonly IReactionRepository _reactionRepository;
    
    public ReactionsController(IReactionRepository reactionRepository)
    {
        _reactionRepository = reactionRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetReactions()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var reactions = await _reactionRepository.GetReactions(Guid.Parse(userId));

        if (reactions == null)
        {
            return Ok(new List<ReactionDto>());
        }
        else
        {
            return Ok(reactions);
        }
    }
    
    [HttpGet("received-requests")]
    [Authorize]
    public async Task<IActionResult> GetReceivedRequests()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var requests = await _reactionRepository.GetReceivedRequests(Guid.Parse(userId));

        if (requests == null)
        {
            return Ok(new List<ReactionDto>());
        }
        else
        {
            return Ok(requests);
        }
    }
    
    [HttpGet("sent-requests")]
    [Authorize]
    public async Task<IActionResult> GetSentRequests()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var requests = await _reactionRepository.GetSentRequests(Guid.Parse(userId));

        if (requests == null)
        {
            return Ok(new List<ReactionDto>());
        }
        else
        {
            return Ok(requests);
        }
    }

}