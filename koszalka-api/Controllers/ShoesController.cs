using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using koszalka_api.Data;
using koszalka_api.DTO;
using koszalka_api.Model;
using koszalka_api.Service;
using Azure;

//Controller using EntityFramework examples
namespace koszalka_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoesController : ControllerBase
    {
        private readonly EntityFrameworkConfigurationContext _context;
        private readonly ShoesService _service;

        public ShoesController(EntityFrameworkConfigurationContext context, ShoesService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoesDTO>>> GetShoes()
        {
            var response = await _service.GetShoes();
            if (response == null)
            {
                return NoContent();
            }
            return await _service.GetShoes();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoesDTO>> GetShoe(long id)
        {
            ActionResult<ShoesDTO> response = await _service.GetShoe(id);
            if (response == null)
            {
                return NoContent();
            }

            return response;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoe(long id, Shoes shoes)
        {
            if (id != shoes.Id)
            {
                return BadRequest();
            }

            Task<Boolean> response = _service.PutShoe(id, shoes);
            {
                if (response.Result.Equals(false))
                {
                    return NotFound();
                }

                return Ok();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Shoes>> PostShoe(Shoes shoes)
        {
            var response = await _service.PostShoe(shoes);

            return CreatedAtAction("GetShoe", new { id = shoes.Id }, shoes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoe(long id)
        {
            var response = await _service.DeleteShoe(id);
            if (response == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        
    }
}
