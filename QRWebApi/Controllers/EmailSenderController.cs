using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QRWebApi.EmailSender;
using QRWebApi.Models;

namespace QRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly Repository _repository;
        private readonly IConfiguration Configuration;
        public EmailSenderController(QRAppDBContext context, IConfiguration configuration)
        {
            _repository = new Repository(context);
            Configuration = configuration;
        }

        // POST: api/EmailSender/SendEmail
        [HttpPost("SendEmail")]
        [Authorize]
        public async Task SendEmail(DictEmailAdressDetails _adress)
        {
            var myKeyValue = Configuration["EmailPass"];
            if (_adress != null)
            {
                var _emailSender = new EmailSender.EmailSender(await _repository.GetEmailConfig(myKeyValue));
                var message = new Message(new string[] { _adress.EmailAdressNotify }, _adress.Subject, _adress.Content_part1, _adress.Content_part2, _adress.Content_part3, _adress.UserSender);
                await _emailSender.SendEmailAsync(message);
            }
        }
    }
}
