using System.Data;
using Npgsql;

namespace galaxy_match_make.Data;
public class DapperContext
{
    private readonly IConfiguration _config;
    public DapperContext(IConfiguration config) => _config = config;

    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
}
