using System.Data;

namespace koszalka_api.Implentation;

public interface IDapperContextImpl
{
    IDbConnection CreateConnection();
}
