using galaxy_match_make.Models;
using galaxy_match_make.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace galaxy_match_make.Controllers;

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
    public async Task<IActionResult> GetAllReactions()
    {
        var reactions = await _reactionRepository.GetAllReactionsAsync();
        return Ok(reactions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReactionById(int id)
    {
        var reaction = await _reactionRepository.GetReactionByIdAsync(id);
        if (reaction == null)
        {
            return NotFound();
        }
        return Ok(reaction);
    }

    [HttpPost]
    public async Task<IActionResult> AddReaction([FromBody] ReactionDto reaction)
    {
        var existingReaction = await _reactionRepository.GetReactionByReactorAndTargetAsync(reaction.ReactorId, reaction.TargetId);
        if (existingReaction != null)
        {
            return BadRequest("You have already reacted to this profile.");
        }
        else
        {
            await _reactionRepository.AddReactionAsync(reaction);
            return CreatedAtAction(nameof(GetReactionById), new { id = reaction.Id }, reaction);
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReaction(int id, [FromBody] ReactionDto reaction)
    {
        if (id != reaction.Id)
        {
            return BadRequest();
        }
        await _reactionRepository.UpdateReactionAsync(reaction);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReaction(int id)
    {
        await _reactionRepository.DeleteReactionAsync(id);
        return NoContent();
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetReactionsByReactor(Guid userId)
    {
        var reactions = await _reactionRepository.GetReactionsByReactorAsync(userId);
        return Ok(reactions);
    }
}