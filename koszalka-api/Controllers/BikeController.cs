using koszalka_api.Model;
using koszalka_api.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public Bike CreateBranch(Bike bike)
        {
            _bikeRepository.Create(bike);
            return bike;
        }
    }
}