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
