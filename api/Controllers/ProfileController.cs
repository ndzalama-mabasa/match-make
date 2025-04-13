using galaxy_match_make.Models;
using galaxy_match_make.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace galaxy_match_make.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;

        public ProfileController(IProfileRepository profileRepository, IUserRepository userRepository)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProfileDto>>> GetAllProfiles()
        {
            var Profiles = await _profileRepository.GetAllProfiles();
            return Ok(Profiles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDto>> GetProfileById(Guid id)
        {
            var Profile = await _profileRepository.GetProfileById(id);
            if (Profile == null)
            {
                return NotFound("This profile does not exist");
            }
            return Ok(Profile);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ProfileDto>> UpdateProfile([FromBody] UpdateProfileDto profile)
        {
            if (profile == null)
            {
                return BadRequest("Profile data is required.");
            }

            var profileClaimToken = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(profileClaimToken) || !Guid.TryParse(profileClaimToken, out Guid userId))
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            var updatedProfile = await _profileRepository.UpdateProfile(userId, profile);
            return Ok(updatedProfile);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProfileDto>> CreateProfile([FromBody] CreateProfileDto profile)
        {
            if (profile == null)
            {
                return BadRequest("Profile data is required.");
            }

            var profileClaimToken = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(profileClaimToken) || !Guid.TryParse(profileClaimToken, out Guid userId))
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            try
            {
                var createdProfile = await _profileRepository.CreateProfile(userId, profile);
                return CreatedAtAction(nameof(GetProfileById), new { id = userId }, createdProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating profile: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<ProfileDto>> GetMyProfile()
        {
            var profileClaimToken  = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(profileClaimToken) || !Guid.TryParse(profileClaimToken, out Guid userId))
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound($"Reactor user with ID {userId} not found");
            }

            var profile = await _profileRepository.GetProfileById(userId);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            return Ok(profile);
        }
    }
}
