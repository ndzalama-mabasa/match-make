using galaxy_match_make.Models;
using galaxy_match_make.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace galaxy_match_make.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository) => _profileRepository = profileRepository;

        [HttpGet]
        public async Task<ActionResult<List<ProfileDto>>> GetAllProfiles()
        {
            var Profiles = await _profileRepository.GetAllProfiles();
            return Ok(Profiles);
        }
    }
}
