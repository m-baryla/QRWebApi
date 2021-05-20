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
        private readonly QRAppDBContext _context;

        public TicketsController(QRAppDBContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet("TicketsHistoriesDetails/")]
        public async Task<ActionResult<IEnumerable<TicketsDetails>>> TicketsHistoriesDetails()
        {
            var query = (from h in _context.Tickets
                join a in _context.DictEmailAdresses on h.IdEmailAdress equals a.Id
                join e in _context.DictEquipments on h.IdEquipment equals e.Id
                join l in _context.DictLocations on h.IdLocation equals l.Id
                join s in _context.DictStatus on h.IdStatus equals s.Id
                select new TicketsDetails
                {
                    Id = h.Id,
                    UserName = h.UserName,
                    Topic = h.Topic,
                    Description = h.Description,
                    Photo = h.Photo,
                    LocationName = l.LocationName,
                    EquipmentName = e.EquipmentName,
                    EmailAdress = a.EmailAdressNotify,
                    Status = s.Status,
                }).ToListAsync();

            return await query;
        }

        // POST: api/Tickets
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(TicketsDetails ticket)
        {
            _context.Tickets.Add(new Ticket()
            {
                UserName = ticket.UserName,
                Topic = ticket.Topic,
                Description = ticket.Description,
                Photo = ticket.Photo,
                IdLocation = _context.DictLocations.Where(l => l.LocationName == ticket.LocationName).Select(l => l.Id).First(),
                IdEquipment = _context.DictEquipments.Where(e => e.EquipmentName == ticket.EquipmentName).Select(e => e.Id).First(),
                IdStatus = _context.DictStatus.Where(s => s.Status == ticket.Status).Select(s => s.Id).First(),
                IdEmailAdress = _context.DictEmailAdresses.Where(e => e.EmailAdressNotify == ticket.EmailAdress).Select(e => e.Id).First()
            });
             await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostTicket), new { id = ticket.Id }, ticket);
        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, TicketsDetails ticket)
        {

            var _ticket = new Ticket()
            {
                Id = ticket.Id,
                UserName = ticket.UserName,
                Topic = ticket.Topic,
                Description = ticket.Description,
                Photo = ticket.Photo,
                IdLocation = _context.DictLocations.Where(l => l.LocationName == ticket.LocationName).Select(l => l.Id).First(),
                IdEquipment = _context.DictEquipments.Where(e => e.EquipmentName == ticket.EquipmentName).Select(e => e.Id).First(),
                IdStatus = _context.DictStatus.Where(s => s.Status == ticket.Status).Select(s => s.Id).First(),
                IdEmailAdress = _context.DictEmailAdresses.Where(e => e.EmailAdressNotify == ticket.EmailAdress).Select(e => e.Id).First()
            };

            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(_ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
