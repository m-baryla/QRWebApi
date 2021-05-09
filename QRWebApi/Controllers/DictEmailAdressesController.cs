using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRWebApi.EmailSender;
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

        // POST: api/DictEmailAdresses/SendEmail
        [HttpPost("SendEmail")]
        public async Task SendEmail(DictEmailAdressDetails _adress)
        {
            var _emailSenderConfig = new EmailSenderConfig();
            _emailSenderConfig.MailFrom = _context.EmailSenderConfigs.Select(u => u.MailFrom).SingleOrDefault();
            _emailSenderConfig.MailHost = _context.EmailSenderConfigs.Select(u => u.MailHost).SingleOrDefault();
            _emailSenderConfig.EmailUser = _context.EmailSenderConfigs.Select(u => u.EmailUser).SingleOrDefault();
            var pass = Convert.FromBase64String(_context.EmailSenderConfigs.Select(u => u.EmailPassword).SingleOrDefault());
            _emailSenderConfig.EmailPassword = Encoding.UTF8.GetString(pass);

            var _emailSender = new EmailSender.EmailSender(_emailSenderConfig);
            var message = new Message(new string[]{ _adress.EmailAdressNotify}, _adress.Subject, _adress.Content_part1, _adress.Content_part2, _adress.Content_part3);
            await _emailSender.SendEmailAsync(message);
        }


        // POST: api/DictEmailAdresses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DictEmailAdress>> PostDictEmailAdress(DictEmailAdress dictEmailAdress)
        {
            _context.DictEmailAdresses.Add(dictEmailAdress);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostDictEmailAdress), new { id = dictEmailAdress.Id }, dictEmailAdress);
        }
        private bool DictEmailAdressExists(int id)
        {
            return _context.DictEmailAdresses.Any(e => e.Id == id);
        }

        //// POST: api/DictEmailAdresses
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<DictEmailAdress>> PostDictEmailAdress(DictEmailAdress dictEmailAdress)
        //{
        //    _context.DictEmailAdresses.Add(new DictEmailAdress()
        //    {
        //        EmailAdressNotify = dictEmailAdress.EmailAdressNotify
        //    });
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDictEmailAdress", new { id = dictEmailAdress.Id }, dictEmailAdress);
        //}

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
