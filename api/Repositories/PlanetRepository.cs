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
        var query = "SELECT id, planet_name FROM planets";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<PlanetDto>(query);
    }

    public async Task<PlanetDto> GetPlanetByIdAsync(int id)
    {
        var query = "SELECT id, planet_name FROM planets WHERE id = @Id";
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<PlanetDto>(query, new { Id = id });
    }

    public async Task AddPlanetAsync(PlanetDto planet)
    {
        var query = "INSERT INTO planets (planet_name) VALUES (@PlanetName)";
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, planet);
    }

    public async Task UpdatePlanetAsync(int id, PlanetDto planet)
    {
        var query = "UPDATE planets SET planet_name = @PlanetName WHERE id = @Id";
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { planet.PlanetName, Id = id });
    }

    public async Task DeletePlanetAsync(int id)
    {
        var query = "DELETE FROM planets WHERE id = @Id";
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }
}
