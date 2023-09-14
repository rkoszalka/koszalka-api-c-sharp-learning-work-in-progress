using koszalka_api.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace koszalka_api.Data
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _iConfiguration;
        private readonly string _connString;
        public DapperContext(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
        }
        public IDbConnection CreateConnection() => new SqlConnection(_iConfiguration.GetConnectionString("DefaultConnection"));
    }
}
