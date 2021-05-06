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
    public class DictStatusController : ControllerBase
    {
        private readonly QRappContext _context;

        public DictStatusController(QRappContext context)
        {
            _context = context;
        }

        // GET: api/DictStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DictStatu>>> GetDictStatus()
        {
            return await _context.DictStatus.ToListAsync();
        }

        ////GET: api/DictStatus/5
        ////[HttpGet("{id}")]
        ////public async Task<ActionResult<DictStatu>> GetDictStatu(int id)
        ////{
        ////    var dictStatu = await _context.DictStatus.FindAsync(id);

        ////    if (dictStatu == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    return dictStatu;
        ////}

        ////PUT: api/DictStatus/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        ////[HttpPut("{id}")]
        ////public async Task<IActionResult> PutDictStatu(int id, DictStatu dictStatu)
        ////{
        ////    if (id != dictStatu.Id)
        ////    {
        ////        return BadRequest();
        ////    }

        ////    _context.Entry(dictStatu).State = EntityState.Modified;

        ////    try
        ////    {
        ////        await _context.SaveChangesAsync();
        ////    }
        ////    catch (DbUpdateConcurrencyException)
        ////    {
        ////        if (!DictStatuExists(id))
        ////        {
        ////            return NotFound();
        ////        }
        ////        else
        ////        {
        ////            throw;
        ////        }
        ////    }

        ////    return NoContent();
        ////}

        ////POST: api/DictStatus
        ////To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        ////[HttpPost]
        ////public async Task<ActionResult<DictStatu>> PostDictStatu(DictStatu dictStatu)
        ////{
        ////    _context.DictStatus.Add(dictStatu);
        ////    await _context.SaveChangesAsync();

        ////    return CreatedAtAction("GetDictStatu", new { id = dictStatu.Id }, dictStatu);
        ////}

        ////DELETE: api/DictStatus/5
        ////[HttpDelete("{id}")]
        ////public async Task<ActionResult<DictStatu>> DeleteDictStatu(int id)
        ////{
        ////    var dictStatu = await _context.DictStatus.FindAsync(id);
        ////    if (dictStatu == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    _context.DictStatus.Remove(dictStatu);
        ////    await _context.SaveChangesAsync();

        ////    return dictStatu;
        ////}

        private bool DictStatuExists(int id)
        {
            return _context.DictStatus.Any(e => e.Id == id);
        }
    }
}
