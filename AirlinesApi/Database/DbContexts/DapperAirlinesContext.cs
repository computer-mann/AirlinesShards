using Npgsql;
using System.Data;

namespace AirlinesApi.Database.DbContexts
{
    public class DapperAirlinesContext
    {
        private readonly IConfiguration _configuration;

        public DapperAirlinesContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("Database"));
        }
    }
}
