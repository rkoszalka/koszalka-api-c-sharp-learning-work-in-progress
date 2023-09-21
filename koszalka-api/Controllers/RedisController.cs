using koszalka_api.Caching;
using koszalka_api.Events.Kafka;
using koszalka_api.Persistence.DTO;
using koszalka_api.Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using StackExchange.Redis;

namespace koszalka_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : Controller
    {

        private readonly ICacheService _iCacheService;
        private readonly IBikeRepository _bikeService;
        private const int CACHE_TIME = 30;

        public RedisController(ICacheService iCacheService, IBikeRepository bikeService)
        {
            _iCacheService = iCacheService;
            _bikeService = bikeService;
        }

        [HttpPost("singleData")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<string> Post([FromQuery] string msg, string key)
        {
                _iCacheService.SetData<string>(key, msg, DateTimeOffset.Now.AddDays(CACHE_TIME));
                return "Cache set";
        }

        [HttpPost("appendData")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public string AppendData([FromQuery] string msg, [FromQuery] string key)
        {
            var resAppendData = _iCacheService.AppendData(key, msg);
            return resAppendData;
        }


        [HttpGet("getData")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public string GetAllRedisTest([FromQuery] string key)
        {
            return _iCacheService.GetData<string>(key).ToString();
        }

        [HttpGet("getAppendedData")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public string GetAppendData([FromQuery] string key)
        {
            return _iCacheService.GetAppendedData(key);
        }
    }
}
