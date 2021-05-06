using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRWebApi.Models;

namespace QRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictEmailAdressesController : ControllerBase
    {
        private readonly QRappContext _context;

        public DictEmailAdressesController(QRappContext context)
        {
            _context = context;
        }

        // GET: api/DictEmailAdresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DictEmailAdress>>> GetDictEmailAdresses()
        {
            return await _context.DictEmailAdresses.ToListAsync();
        }
        private bool DictEmailAdressExists(int id)
        {
            return _context.DictEmailAdresses.Any(e => e.Id == id);
        }

        //// GET: api/DictEmailAdresses/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<DictEmailAdress>> GetDictEmailAdress(int id)
        //{
        //    var dictEmailAdress = await _context.DictEmailAdresses.FindAsync(id);

        //    if (dictEmailAdress == null)
        //    {
        //        return NotFound();
        //    }

        //    return dictEmailAdress;
        //}

        //// PUT: api/DictEmailAdresses/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDictEmailAdress(int id, DictEmailAdress dictEmailAdress)
        //{
        //    if (id != dictEmailAdress.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(dictEmailAdress).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DictEmailAdressExists(id))
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

        //// POST: api/DictEmailAdresses
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<DictEmailAdress>> PostDictEmailAdress(DictEmailAdress dictEmailAdress)
        //{
        //    _context.DictEmailAdresses.Add(dictEmailAdress);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDictEmailAdress", new { id = dictEmailAdress.Id }, dictEmailAdress);
        //}

        //// DELETE: api/DictEmailAdresses/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DictEmailAdress>> DeleteDictEmailAdress(int id)
        //{
        //    var dictEmailAdress = await _context.DictEmailAdresses.FindAsync(id);
        //    if (dictEmailAdress == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.DictEmailAdresses.Remove(dictEmailAdress);
        //    await _context.SaveChangesAsync();

        //    return dictEmailAdress;
        //}

    }
}
