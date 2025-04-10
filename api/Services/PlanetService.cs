using galaxy_match_make.Models;
using galaxy_match_make.Repositories;

namespace galaxy_match_make.Services;

public class PlanetService : IPlanetService
{
    private readonly IPlanetRepository _repository;

    public PlanetService(IPlanetRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PlanetDto>> GetAllPlanetsAsync()
    {
        var planets = await _repository.GetAllPlanetsAsync();
        return planets;
    }
}
