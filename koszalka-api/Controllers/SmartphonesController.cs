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
    [Route("[controller]")]
    [ApiController]
    public class SmartphonesController : ControllerBase
    {
        private readonly EntityFrameworkConfigurationContext _context;

        public SmartphonesController(EntityFrameworkConfigurationContext context)
        {
            _context = context;
        }

        // GET: api/Smartphones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Smartphone>>> GetSmartphone()
        {
          if (_context.Smartphone == null)
          {
              return NotFound();
          }
            return await _context.Smartphone.ToListAsync();
        }

        // GET: api/Smartphones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Smartphone>> GetSmartphone(long id)
        {
          if (_context.Smartphone == null)
          {
              return NotFound();
          }
            var smartphone = await _context.Smartphone.FindAsync(id);

            if (smartphone == null)
            {
                return NotFound();
            }

            return smartphone;
        }

        // PUT: api/Smartphones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmartphone(long id, Smartphone smartphone)
        {
            if (id != smartphone.Id)
            {
                return BadRequest();
            }

            _context.Entry(smartphone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmartphoneExists(id))
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

        // POST: api/Smartphones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Smartphone>> PostSmartphone(Smartphone smartphone)
        {
          if (_context.Smartphone == null)
          {
              return Problem("Entity set 'EntityFrameworkConfigurationContext.Smartphone'  is null.");
          }
            _context.Smartphone.Add(smartphone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSmartphone", new { id = smartphone.Id }, smartphone);
        }

        // DELETE: api/Smartphones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmartphone(long id)
        {
            if (_context.Smartphone == null)
            {
                return NotFound();
            }
            var smartphone = await _context.Smartphone.FindAsync(id);
            if (smartphone == null)
            {
                return NotFound();
            }

            _context.Smartphone.Remove(smartphone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SmartphoneExists(long id)
        {
            return (_context.Smartphone?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
