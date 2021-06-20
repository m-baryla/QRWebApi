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
    public class DictLocationsController : ControllerBase
    {
        private readonly Repository _repository;

        public DictLocationsController(QRAppDBContext context)
        {
            _repository = new Repository(context);
        }

        // GET: api/DictLocations
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DictLocation>>> GetDictLocations()
        {
            return await _repository.GetDictLocations();
        }

        // POST: api/DictLocations
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<DictLocation>> PostDictLocation(DictLocation dictLocation)
        {
            if (dictLocation != null)
            {
                await _repository.PostDictLocation(dictLocation);

                return CreatedAtAction(nameof(PostDictLocation), new { id = dictLocation.Id }, dictLocation);
            }
            return NotFound();
        }
    }
}
