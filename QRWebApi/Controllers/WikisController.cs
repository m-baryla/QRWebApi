using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using QRWebApi.Models;

namespace QRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WikisController : ControllerBase
    {
        private readonly QRappContext _context;

        public WikisController(QRappContext context)
        {
            _context = context;
        }

        // GET: api/Wikis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wiki>>> GetWikis()
        {
            return await _context.Wikis.ToListAsync();
        }

        // GET: /api/Wikis/GetWikiDetail/
        [HttpGet("GetWikiDetail/")]
        public async Task<ActionResult<IEnumerable<WikiDetails>>> GetWikiDetail()
        {
            var query = (from w in _context.Wikis
                                    join l in _context.DictLocations on w.IdLocation equals l.Id
                                    join e in _context.DictEquipments on w.IdEquipment equals e.Id
                                    select new WikiDetails
                                    {
                                        LocationName = l.LocationName,
                                        EquipmentName = e.EquipmentName,
                                        Topic = w.Topic,
                                        Description = w.Description,
                                        Photo = w.Photo
                                    }).ToListAsync();

            return await query;
        }

        // POST: api/Wikis
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Wiki>> PostWiki(WikiDetails wiki)
        {
            _context.Wikis.Add(new Wiki()
            {
                Topic = wiki.Topic,
                Description = wiki.Description,
                Photo = wiki.Photo,
                IdLocation = _context.DictLocations.Where(l => l.LocationName == wiki.LocationName).Select(l => l.Id).First(),
                IdEquipment = _context.DictEquipments.Where(e => e.EquipmentName == wiki.EquipmentName).Select(e => e.Id).First(),
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWiki", new { id = wiki.Id }, wiki);
        }

        private bool WikiExists(int id)
        {
            return _context.Wikis.Any(e => e.Id == id);
        }

        //// GET: api/Wikis/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Wiki>> GetWiki(int id)
        //{
        //    var wiki = await _context.Wikis.FindAsync(id);

        //    if (wiki == null)
        //    {
        //        return NotFound();
        //    }

        //    return wiki;
        //}

        //// PUT: api/Wikis/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWiki(int id, Wiki wiki)
        //{
        //    if (id != wiki.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(wiki).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WikiExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Wikis
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Wiki>> PostWiki(Wiki wiki)
        //{
        //    _context.Wikis.Add(wiki);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetWiki", new { id = wiki.Id }, wiki);
        //}

        //// DELETE: api/Wikis/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Wiki>> DeleteWiki(int id)
        //{
        //    var wiki = await _context.Wikis.FindAsync(id);
        //    if (wiki == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Wikis.Remove(wiki);
        //    await _context.SaveChangesAsync();

        //    return wiki;
        //}

    }
}
