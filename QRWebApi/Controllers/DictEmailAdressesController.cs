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
        private readonly Repository _repository;

        public DictEmailAdressesController(QRAppDBContext context)
        {
            _repository = new Repository(context);
        }

        // GET: api/DictEmailAdresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DictEmailAdress>>> GetDictEmailAdresses()
        {
            return await _repository.GetDictEmailAdresses();
            //return await _context.DictEmailAdresses.ToListAsync();
        }

        // POST: api/DictEmailAdresses/SendEmail
        [HttpPost("SendEmail")]
        public async Task SendEmail(DictEmailAdressDetails _adress)
        {
            if (_adress != null)
            {
                //var query = (from h in _context.EmailSenderConfigs
                //    select new EmailSenderConfig
                //    {
                //        MailFrom = h.MailFrom,
                //        MailHost = h.MailHost,
                //        EmailUser = h.EmailUser,
                //        EmailPassword = Encoding.UTF8.GetString(Convert.FromBase64String(h.EmailPassword))

                //    }).SingleOrDefault();

                var _emailSender = new EmailSender.EmailSender(await _repository.GetEmailConfig());
                var message = new Message(new string[] { _adress.EmailAdressNotify }, _adress.Subject, _adress.Content_part1, _adress.Content_part2, _adress.Content_part3, _adress.UserSender);
                await _emailSender.SendEmailAsync(message);
            }
        }


        // POST: api/DictEmailAdresses
        [HttpPost]
        public async Task<ActionResult<DictEmailAdress>> PostDictEmailAdress(DictEmailAdress dictEmailAdress)
        {
            //_context.DictEmailAdresses.Add(dictEmailAdress);
            //await _context.SaveChangesAsync();
            await _repository.PostDictEmailAdress(dictEmailAdress);

            return CreatedAtAction(nameof(PostDictEmailAdress), new { id = dictEmailAdress.Id }, dictEmailAdress);
        }
        //private bool DictEmailAdressExists(int id)
        //{
        //    return _context.DictEmailAdresses.Any(e => e.Id == id);
        //}

    }
}
