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
    public class TicketsHistoriesController : ControllerBase
    {
        private readonly QRappContext _context;

        public TicketsHistoriesController(QRappContext context)
        {
            _context = context;
        }

        // GET: api/TicketsHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketsHistory>>> GetTicketsHistories()
        {
            return await _context.TicketsHistories.ToListAsync();
        }

        // GET: api/TicketsHistories/TicketsHistoriesDetails
        [HttpGet("TicketsHistoriesDetails/")]
        public async Task<ActionResult<IEnumerable<TicketsHistoryDetails>>> TicketsHistoriesDetails()
        {

            var query = (from h in _context.TicketsHistories
                join a in _context.DictEmailAdresses on h.IdEmailAdress equals a.Id
                join e in _context.DictEquipments on h.IdEquipment equals e.Id
                join l in _context.DictLocations on h.IdLocation equals l.Id
                join s in _context.DictStatus on h.IdStatus equals s.Id
                join u in _context.Users on h.IdUser equals u.Id
                 select new TicketsHistoryDetails
                    {
                      UserName = u.Login,
                      Topic = h.Topic,
                      Description = h.Description,
                      Photo = h.Photo,
                      LocationName = l.LocationName,
                      EquipmentName = e.EquipmentName,
                      EmailAdress = a.EmailAdressNotify,
                      Status = s.Status,
                      IsAnonymous = h.IsAnonymous
                    }).ToListAsync();

            return await query;
        }

        // GET: api/TicketsHistories/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TicketsHistory>> GetTicketsHistory(int id)
        //{
        //    var ticketsHistory = await _context.TicketsHistories.FindAsync(id);

        //    if (ticketsHistory == null)
        //    {
        //        return NotFound();
        //    }

        //    return ticketsHistory;
        //}

        // PUT: api/TicketsHistories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTicketsHistory(int id, TicketsHistory ticketsHistory)
        //{
        //    if (id != ticketsHistory.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(ticketsHistory).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TicketsHistoryExists(id))
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

        // POST: api/TicketsHistories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<TicketsHistory>> PostTicketsHistory(TicketsHistory ticketsHistory)
        //{
        //    _context.TicketsHistories.Add(ticketsHistory);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTicketsHistory", new { id = ticketsHistory.Id }, ticketsHistory);
        //}

        // DELETE: api/TicketsHistories/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<TicketsHistory>> DeleteTicketsHistory(int id)
        //{
        //    var ticketsHistory = await _context.TicketsHistories.FindAsync(id);
        //    if (ticketsHistory == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TicketsHistories.Remove(ticketsHistory);
        //    await _context.SaveChangesAsync();

        //    return ticketsHistory;
        //}

        private bool TicketsHistoryExists(int id)
        {
            return _context.TicketsHistories.Any(e => e.Id == id);
        }
    }
}
