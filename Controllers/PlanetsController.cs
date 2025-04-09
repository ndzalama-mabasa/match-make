using galaxy_match_make.Models;
using galaxy_match_make.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace galaxy_match_make.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetRepository _repository;
        public PlanetsController(IPlanetRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<ActionResult<PlanetDto>> GetAllSchools()
        {
            var planets = await _repository.GetAllPlanetsAsync();
            return Ok(planets);
        }
    }
}
