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
        private readonly QRAppDBContext _context;

        public WikisController(QRAppDBContext context)
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

            return CreatedAtAction(nameof(PostWiki), new { id = wiki.Id }, wiki);
        }

        private bool WikiExists(int id)
        {
            return _context.Wikis.Any(e => e.Id == id);
        }

    }
}
