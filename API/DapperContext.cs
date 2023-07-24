using System.Data;
using API.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace API
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IOptions<DatabaseConnectionOptions> options)
        {
            _connectionString = options.Value.DefaultConnection;
        }
        
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}