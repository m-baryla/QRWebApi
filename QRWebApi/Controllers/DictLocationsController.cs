using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRWebApi.Models;

namespace QRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictLocationsController : ControllerBase
    {
        private readonly QRappContext _context;

        public DictLocationsController(QRappContext context)
        {
            _context = context;
        }

        // GET: api/DictLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DictLocation>>> GetDictLocations()
        {
            return await _context.DictLocations.ToListAsync();
        }

        // GET: api/DictLocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DictLocation>> GetDictLocation(int id)
        {
            var dictLocation = await _context.DictLocations.FindAsync(id);

            if (dictLocation == null)
            {
                return NotFound();
            }

            return dictLocation;
        }

        // PUT: api/DictLocations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDictLocation(int id, DictLocation dictLocation)
        {
            if (id != dictLocation.Id)
            {
                return BadRequest();
            }

            _context.Entry(dictLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DictLocationExists(id))
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

        // POST: api/DictLocations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DictLocation>> PostDictLocation(DictLocation dictLocation)
        {
            _context.DictLocations.Add(dictLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDictLocation", new { id = dictLocation.Id }, dictLocation);
        }

        // DELETE: api/DictLocations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DictLocation>> DeleteDictLocation(int id)
        {
            var dictLocation = await _context.DictLocations.FindAsync(id);
            if (dictLocation == null)
            {
                return NotFound();
            }

            _context.DictLocations.Remove(dictLocation);
            await _context.SaveChangesAsync();

            return dictLocation;
        }

        private bool DictLocationExists(int id)
        {
            return _context.DictLocations.Any(e => e.Id == id);
        }
    }
}
