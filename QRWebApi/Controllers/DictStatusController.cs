using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QRWebApi.Models;

namespace QRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictStatusController : ControllerBase
    {
        private readonly Repository _repository;

        public DictStatusController(QRAppDBContext context)
        {
            _repository = new Repository(context);
        }

        // GET: api/DictStatus
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DictStatus>>> GetDictStatus()
        {
            return await _repository.GetDictStatus();
        }
    }
}
