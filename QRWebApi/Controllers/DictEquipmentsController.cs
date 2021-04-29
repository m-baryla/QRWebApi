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
    public class DictEquipmentsController : ControllerBase
    {
        private readonly QRappContext _context;

        public DictEquipmentsController(QRappContext context)
        {
            _context = context;
        }

        // GET: api/DictEquipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DictEquipment>>> GetDictEquipments()
        {
            return await _context.DictEquipments.ToListAsync();
        }

        // GET: api/DictEquipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DictEquipment>> GetDictEquipment(int id)
        {
            var dictEquipment = await _context.DictEquipments.FindAsync(id);

            if (dictEquipment == null)
            {
                return NotFound();
            }

            return dictEquipment;
        }

        // PUT: api/DictEquipments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDictEquipment(int id, DictEquipment dictEquipment)
        {
            if (id != dictEquipment.Id)
            {
                return BadRequest();
            }

            _context.Entry(dictEquipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DictEquipmentExists(id))
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

        // POST: api/DictEquipments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DictEquipment>> PostDictEquipment(DictEquipment dictEquipment)
        {
            _context.DictEquipments.Add(dictEquipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDictEquipment", new { id = dictEquipment.Id }, dictEquipment);
        }

        // DELETE: api/DictEquipments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DictEquipment>> DeleteDictEquipment(int id)
        {
            var dictEquipment = await _context.DictEquipments.FindAsync(id);
            if (dictEquipment == null)
            {
                return NotFound();
            }

            _context.DictEquipments.Remove(dictEquipment);
            await _context.SaveChangesAsync();

            return dictEquipment;
        }

        private bool DictEquipmentExists(int id)
        {
            return _context.DictEquipments.Any(e => e.Id == id);
        }
    }
}
