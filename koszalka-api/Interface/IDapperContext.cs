using System.Data;

namespace koszalka_api.Interface;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}