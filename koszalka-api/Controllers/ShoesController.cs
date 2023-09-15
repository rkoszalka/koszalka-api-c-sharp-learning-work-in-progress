using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using koszalka_api.Data;
using koszalka_api.Model;

namespace koszalka_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesController : ControllerBase
    {
        private readonly EntityFrameworkConfigurationContext _context;

        public ShoesController(EntityFrameworkConfigurationContext context)
        {
            _context = context;
        }

        // GET: api/Shoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shoes>>> GetShoes()
        {
          if (_context.Shoes == null)
          {
              return NotFound();
          }
            return await _context.Shoes.ToListAsync();
        }

        // GET: api/Shoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shoes>> GetShoe(long id)
        {
          if (_context.Shoes == null)
          {
              return NotFound();
          }
            var shoe = await _context.Shoes.FindAsync(id);

            if (shoe == null)
            {
                return NotFound();
            }

            return shoe;
        }

        // PUT: api/Shoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoe(long id, Shoes shoes)
        {
            if (id != shoes.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Shoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shoes>> PostShoe(Shoes shoes)
        {
          if (_context.Shoes == null)
          {
              return Problem("Entity set 'EntityFrameworkConfigurationContext.Shoes'  is null.");
          }
            _context.Shoes.Add(shoes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoe", new { id = shoes.Id }, shoes);
        }

        // DELETE: api/Shoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoe(long id)
        {
            if (_context.Shoes == null)
            {
                return NotFound();
            }
            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe == null)
            {
                return NotFound();
            }

            _context.Shoes.Remove(shoe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoeExists(long id)
        {
            return (_context.Shoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
