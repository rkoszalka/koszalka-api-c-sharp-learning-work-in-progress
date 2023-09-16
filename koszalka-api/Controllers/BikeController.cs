using koszalka_api.Model;
using koszalka_api.Repository;
using Microsoft.AspNetCore.Mvc;

// Controller using Dapper ORM examples
namespace koszalka_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BikeController : Controller
    {
        private readonly IBikeRepository _bikeRepository;

        public BikeController(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBike(Bike bike)
        {
            var rowsCreated = await _bikeRepository.Create(bike);
            if (rowsCreated.Equals(1))
            {
                return Ok();
            };

            return UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void DeleteBike(int id)
        {
            _bikeRepository.Delete(id);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Bike> GetBikeById(long id)
        {
            return await _bikeRepository.GetByIdAsync(id);
        }


        [HttpGet("GetAllBikes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<Bike>> GetAllBikes()
        {
            return await _bikeRepository.GetAllAsync();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBike(Bike bike)
        {
            var rowsCreated = await _bikeRepository.Update(bike);
            if (rowsCreated.Equals(1))
            {
                return Ok();
            };
            return UnprocessableEntity();
        }

    }
}