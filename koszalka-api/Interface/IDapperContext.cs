using System.Data;

namespace koszalka_api.Interface;

public interface IDapperContextImpl
{
    IDbConnection CreateConnection();
}