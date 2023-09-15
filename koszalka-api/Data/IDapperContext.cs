using System.Data;

namespace koszalka_api.Data;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}