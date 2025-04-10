using Dapper;
using galaxy_match_make.Data;
using galaxy_match_make.Models;

namespace galaxy_match_make.Repositories;

public class PlanetRepository : IPlanetRepository
{
    private readonly DapperContext _context;
    public PlanetRepository(DapperContext context) => _context = context;

    public async Task<IEnumerable<PlanetDto>> GetAllPlanetsAsync()
    {
        var query = "SELECT Id, planet_name FROM Planets";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<PlanetDto>(query);
    }
}
