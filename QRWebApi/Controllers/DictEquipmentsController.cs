using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QRWebApi.Models;

namespace QRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictEquipmentsController : ControllerBase
    {
        private readonly Repository _repository;

        public DictEquipmentsController(QRAppDBContext context)
        {
            _repository = new Repository(context);
        }

        // GET: api/DictEquipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DictEquipment>>> GetDictEquipments()
        {
            return await _repository.GetDictEquipments();
        }

        // POST: api/DictEquipments
        [HttpPost]
        public async Task<ActionResult<DictEquipment>> PostDictEquipment(DictEquipment dictEquipment)
        {
            if (dictEquipment != null)
            {
                await _repository.PostDictEquipment(dictEquipment);

                return CreatedAtAction(nameof(PostDictEquipment), new { id = dictEquipment.Id }, dictEquipment);
            }
            return NotFound();
        }
    }
}
