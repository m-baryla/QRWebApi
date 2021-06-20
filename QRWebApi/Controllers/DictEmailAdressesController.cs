using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web.Resource;
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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DictEmailAdress>>> GetDictEmailAdresses()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope("Contact.View");
            return await _repository.GetDictEmailAdresses();
        }

        // POST: api/DictEmailAdresses
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<DictEmailAdress>> PostDictEmailAdress(DictEmailAdress dictEmailAdress)
        {
            if (dictEmailAdress != null)
            {
                await _repository.PostDictEmailAdress(dictEmailAdress);

                return CreatedAtAction(nameof(PostDictEmailAdress), new {id = dictEmailAdress.Id}, dictEmailAdress);
            }
            return NotFound();
        }
    }
}
