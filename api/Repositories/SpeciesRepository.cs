using Dapper;
using galaxy_match_make.Models;
using System.Data;

namespace galaxy_match_make.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly IDbConnection _dbConnection;

    public SpeciesRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<SpeciesDto>> GetAllSpeciesAsync()
    {
        const string query = "SELECT id, species_name as SpeciesName FROM species";
        return await _dbConnection.QueryAsync<SpeciesDto>(query);
    }

    public async Task<SpeciesDto?> GetSpeciesByIdAsync(int id)
    {
        const string query = "SELECT id, species_name as SpeciesName FROM species WHERE id = @Id";
        return await _dbConnection.QuerySingleOrDefaultAsync<SpeciesDto>(query, new { Id = id });
    }
}