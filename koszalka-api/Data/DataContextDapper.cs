using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using koszalka_api.Interface;


namespace koszalka_api.Data
{

    class DataContextDapper 
    {

        private readonly IConfiguration _config;

        public DataContextDapper(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection("Server=localhost; Database=DotNetCourseDatabase; Trusted_Connection=true; TrustServerCertificate=true");
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection("Server=localhost; Database=DotNetCourseDatabase; Trusted_Connection=true; TrustServerCertificate=true");
            return dbConnection.QuerySingle<T>(sql);
        }
    }
}