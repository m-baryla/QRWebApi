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
    public class DictTicketTypesController : ControllerBase
    {
        private readonly QRAppDBContext _context;

        public DictTicketTypesController(QRAppDBContext context)
        {
            _context = context;
        }

        // GET: api/DictTicketTypes/GetDictTicketTypes
        [HttpGet("GetDictTicketTypes")]
        public async Task<ActionResult<IEnumerable<DictTicketTypeDetail>>> GetDictTicketTypes()
        {
            var query = (from a in _context.DictTicketTypes
                join b in _context.Tickets on a.Id equals b.IdTicketType
                group a by a.Type into g
                select new DictTicketTypeDetail
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            return await query;
        }

        // GET: api/DictTicketTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DictTicketType>> GetDictTicketType(int id)
        {
            var dictTicketType = await _context.DictTicketTypes.FindAsync(id);

            if (dictTicketType == null)
            {
                return NotFound();
            }

            return dictTicketType;
        }


        private bool DictTicketTypeExists(int id)
        {
            return _context.DictTicketTypes.Any(e => e.Id == id);
        }
    }
}
