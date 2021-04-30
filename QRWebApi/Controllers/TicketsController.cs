using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using QRWebApi.Models;

namespace QRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly QRappContext _context;

        public TicketsController(QRappContext context)
        {
            _context = context;
        }

        // POST: api/Tickets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(TicketsDetails ticket)
        {
            _context.Tickets.Add(new Ticket()
            {
                IdUser = _context.Users.Where(u => u.Login == ticket.UserName).Select(u => u.Id).First(),
                Topic = ticket.Topic,
                Description = ticket.Description,
                Photo = ticket.Photo,
                IdLocation = _context.DictLocations.Where(l => l.LocationName == ticket.LocationName).Select(l => l.Id).First(),
                IdEquipment = _context.DictEquipments.Where(e => e.EquipmentName == ticket.EquipmentName).Select(e => e.Id).First(),
                IdStatus = _context.DictStatus.Where(s => s.Status == ticket.Status).Select(s => s.Id).First(),
                IdEmailAdress = _context.DictEmailAdresses.Where(e => e.EmailAdressNotify == ticket.EmailAdress).Select(e => e.Id).First(),
                IsAnonymous = ticket.IsAnonymous
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }

        //// GET: api/Tickets
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        //{
        //    return await _context.Tickets.ToListAsync();
        //}

        //// GET: api/Tickets/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Ticket>> GetTicket(int id)
        //{
        //    var ticket = await _context.Tickets.FindAsync(id);

        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    return ticket;
        //}

        //// PUT: api/Tickets/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        //{
        //    if (id != ticket.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(ticket).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TicketExists(id))
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



        //// DELETE: api/Tickets/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Ticket>> DeleteTicket(int id)
        //{
        //    var ticket = await _context.Tickets.FindAsync(id);
        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Tickets.Remove(ticket);
        //    await _context.SaveChangesAsync();

        //    return ticket;
        //}
    }
}
