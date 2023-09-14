using koszalka_api.Data;
using koszalka_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace koszalka_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }


    [HttpGet("GetUser/{name}")]
    public IEnumerable<UserModel> GetUsers(string name)
    {
        return Enumerable.Range(1, 5).Select(index => new UserModel
            {
                name = name,
                age = 34,
            })
            .ToArray();
    }
}
