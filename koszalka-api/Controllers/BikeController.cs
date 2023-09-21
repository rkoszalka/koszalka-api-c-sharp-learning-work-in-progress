using AutoMapper;
using koszalka_api.Persistence.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using koszalka_api.Caching;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using static Dapper.SqlMapper;
using koszalka_api.Persistence.Repository;

// Controller using Dapper ORM examples
namespace koszalka_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BikeController : Controller
    {
        private readonly IBikeRepository _bikeService;

        public BikeController(IBikeRepository bikeService)
        {
            _bikeService = bikeService;
        }



        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBike(BikeDTO bike)
        {
            var rowsCreated = await _bikeService.Create(bike);
            if (rowsCreated.Equals(1))
            {
                return Ok();
            };

            return UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void DeleteBike(int id)
        {
            _bikeService.Delete(id);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<BikeDTO> GetBikeById(long id)
        {
            return await _bikeService.GetByIdAsync(id);
        }


        [HttpGet("GetAllBikes")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<BikeDTO>> GetAllBikes()
        {
            return await _bikeService.GetAllAsync();
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBike(BikeDTO bike)
        {
            var rowsCreated = await _bikeService.Update(bike);
            if (rowsCreated.Equals(1))
            {
                return Ok();
            };
            return UnprocessableEntity();
        }

    }
}