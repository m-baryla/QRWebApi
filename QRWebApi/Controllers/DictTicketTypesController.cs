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

        // GET: api/DictTicketTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DictTicketType>>> GetDictTicketTypes()
        {
            return await _context.DictTicketTypes.ToListAsync();
        }

        // GET: api/DictTicketTypes/GetDictTicketTypesActive
        [HttpGet("GetDictTicketTypesActive")]
        public async Task<ActionResult<IEnumerable<DictTicketTypeDetail>>> GetDictTicketTypesActive()
        {
            var query = (from a in _context.DictTicketTypes
                join b in _context.Tickets on a.Id equals b.IdTicketType
                join s in _context.DictStatus on b.IdStatus equals s.Id
                where s.Status == "Active"
                group a by a.Type into g
                select new DictTicketTypeDetail
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            return await query;
        }

        // GET: api/DictTicketTypes/GetDictTicketTypesNotActive
        [HttpGet("GetDictTicketTypesNotActive")]
        public async Task<ActionResult<IEnumerable<DictTicketTypeDetail>>> GetDictTicketTypesNotActive()
        {
            var query = (from a in _context.DictTicketTypes
                join b in _context.Tickets on a.Id equals b.IdTicketType
                join s in _context.DictStatus on b.IdStatus equals s.Id
                where s.Status == "Not Active" 
                group a by a.Type into g
                select new DictTicketTypeDetail
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            return await query;
        }

        // GET: api/DictTicketTypes/GetDictTicketTypesAllActive
        [HttpGet("GetDictTicketTypesAllActive")]
        public async Task<ActionResult<IEnumerable<DictTicketTypeDetail>>> GetDictTicketTypesAllActive()
        {
            var query = (from a in _context.DictStatus
                join b in _context.Tickets on a.Id equals b.IdStatus
                where a.Status == "Active" 
                group a by a.Status into g
                select new DictTicketTypeDetail
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            return await query;
        }

        // GET: api/DictTicketTypes/GetDictTicketTypesAllNotActive
        [HttpGet("GetDictTicketTypesAllNotActive")]
        public async Task<ActionResult<IEnumerable<DictTicketTypeDetail>>> GetDictTicketTypesAllNotActive()
        {
            var query = (from a in _context.DictStatus
                join b in _context.Tickets on a.Id equals b.IdStatus
                where a.Status == "Not Active"
                group a by a.Status into g
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
