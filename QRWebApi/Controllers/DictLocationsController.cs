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
        private readonly QRAppDBContext _context;

        public DictLocationsController(QRAppDBContext context)
        {
            _context = context;
        }

        // GET: api/DictLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DictLocation>>> GetDictLocations()
        {
            return await _context.DictLocations.ToListAsync();
        }

        private bool DictLocationExists(int id)
        {
            return _context.DictLocations.Any(e => e.Id == id);
        }

        // POST: api/DictLocations
        [HttpPost]
        public async Task<ActionResult<DictLocation>> PostDictLocation(DictLocation dictLocation)
        {
            _context.DictLocations.Add(dictLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostDictLocation), new { id = dictLocation.Id }, dictLocation);
        }
    }
}
