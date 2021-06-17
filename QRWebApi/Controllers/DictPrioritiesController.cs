using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QRWebApi.Models;

namespace QRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictPrioritiesController : ControllerBase
    {
        private readonly Repository _repository;

        public DictPrioritiesController(QRAppDBContext context)
        {
            _repository = new Repository(context);
        }

        // GET: api/DictPriorities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DictPriority>>> GetDictPriorities()
        {
            return await _repository.GetDictPriorities();
        }

        // GET: api/DictPriorities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DictPriority>> GetDictPriority(int id)
        {
            if (id != null)
            {
                var dictPriority = await _repository.GetDictPriority(id);

                return dictPriority;
            }
            return NotFound();
        }
    }
}
