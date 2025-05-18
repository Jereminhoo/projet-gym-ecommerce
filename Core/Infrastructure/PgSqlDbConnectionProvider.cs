using Npgsql;
using System.Data;

namespace Projet_salle_de_gym.Core.Infrastructure
{
    public class PgSqlDbConnectionProvider : IDbConnectionProvider
    {
        private readonly string _connectionString;

        public PgSqlDbConnectionProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<IDbConnection> CreateConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}