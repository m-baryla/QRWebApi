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
    public class DictTicketTypesController : ControllerBase
    {
        private readonly Repository _repository;

        public DictTicketTypesController(QRAppDBContext context)
        {
            _repository = new Repository(context);
        }

        // GET: api/DictTicketTypes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DictTicketType>>> GetDictTicketTypes()
        {
            return await _repository.GetDictTicketTypes();
        }

        // GET: api/DictTicketTypes/GetDictTicketTypesActive
        [HttpGet("GetDictTicketTypesActive")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DictTicketTypeDetail>>> GetDictTicketTypesActive()
        {
            return await _repository.GetDictTicketTypesActive();
        }

        // GET: api/DictTicketTypes/GetDictTicketTypesNotActive
        [HttpGet("GetDictTicketTypesNotActive")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DictTicketTypeDetail>>> GetDictTicketTypesNotActive()
        {
            return await _repository.GetDictTicketTypesNotActive();
        }

        // GET: api/DictTicketTypes/GetDictTicketTypesAllActive
        [HttpGet("GetDictTicketTypesAllActive")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DictTicketTypeDetail>>> GetDictTicketTypesAllActive()
        {
            return await _repository.GetDictTicketTypesAllActive();
        }

        // GET: api/DictTicketTypes/GetDictTicketTypesAllNotActive
        [HttpGet("GetDictTicketTypesAllNotActive")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DictTicketTypeDetail>>> GetDictTicketTypesAllNotActive()
        {
            return await _repository.GetDictTicketTypesAllNotActive();
        }

        // GET: api/DictTicketTypes/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<DictTicketType>> GetDictTicketType(int id)
        {
            if (id != null)
            {
                var dictTicketType = await _repository.GetDictTicketType(id);

                return dictTicketType;
            }
            return NotFound();
        }
     }
}
