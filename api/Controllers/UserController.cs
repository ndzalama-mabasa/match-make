using System.Security.Claims;
using galaxy_match_make.Models;
using galaxy_match_make.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace galaxy_match_make.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository) => _userRepository = userRepository;

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDto>> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("This user does not exist");
            }
            return Ok(user);
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult UserOnly()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(userId);
        }
    }
}
