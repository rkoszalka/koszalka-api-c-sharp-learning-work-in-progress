using Microsoft.Data.SqlClient;
using System.Data;

namespace koszalka_api.Persistence.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _iConfiguration;
        public DapperContext(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
        }
        public IDbConnection CreateConnection() => new SqlConnection(_iConfiguration.GetConnectionString("DefaultConnection"));
    }
}