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
        private readonly QRAppDBContext _context;

        public DictEmailAdressesController(QRAppDBContext context)
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
            var message = new Message(new string[]{ _adress.EmailAdressNotify}, _adress.Subject, _adress.Content_part1, _adress.Content_part2, _adress.Content_part3,_adress.UserSender);
            await _emailSender.SendEmailAsync(message);
        }


        // POST: api/DictEmailAdresses
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

    }
}
