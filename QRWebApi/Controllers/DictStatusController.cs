using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<DictStatu>>> GetDictStatus()
        {
            return await _repository.GetDictStatus();
        }
    }
}
