using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QRWebApi.EmailSender;
using QRWebApi.Models;

namespace QRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly Repository _repository;

        public EmailSenderController(QRAppDBContext context)
        {
            _repository = new Repository(context);
        }

        // POST: api/EmailSender/SendEmail
        [HttpPost("SendEmail")]
        public async Task SendEmail(DictEmailAdressDetails _adress)
        {
            if (_adress != null)
            {
                var _emailSender = new EmailSender.EmailSender(await _repository.GetEmailConfig());
                var message = new Message(new string[] { _adress.EmailAdressNotify }, _adress.Subject, _adress.Content_part1, _adress.Content_part2, _adress.Content_part3, _adress.UserSender);
                await _emailSender.SendEmailAsync(message);
            }
        }
    }
}
