using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QRWebApi.Models;

namespace QRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly Repository _repository;

        public TicketsController(QRAppDBContext context)
        {
            _repository = new Repository(context);
        }

        // GET: api/Tickets
        [HttpGet("TicketsHistoriesDetails/")]
        public async Task<ActionResult<IEnumerable<TicketsDetails>>> TicketsHistoriesDetails()
        {
            return await _repository.TicketsHistoriesDetails();
        }

        // POST: api/Tickets
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(TicketsDetails ticket)
        {
            if (ticket != null)
            {
                await _repository.PostTicket(ticket);

                return CreatedAtAction(nameof(PostTicket), new { id = ticket.Id }, ticket);
            }
            return NotFound();
        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task PutTicket(int id, TicketsDetails ticket)
        {
            if (ticket != null && id != null)
            {
                await _repository.PutTicket(id, ticket);
            }
        }
    }
}
