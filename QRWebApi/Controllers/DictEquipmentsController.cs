using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //return await _context.DictEquipments.ToListAsync();
            return await _repository.GetDictEquipments();

        }
        //private bool DictEquipmentExists(int id)
        //{
        //    return _context.DictEquipments.Any(e => e.Id == id);
        //}

        // POST: api/DictEquipments
        [HttpPost]
        public async Task<ActionResult<DictEquipment>> PostDictEquipment(DictEquipment dictEquipment)
        {
            //_context.DictEquipments.Add(dictEquipment);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(PostDictEquipment), new { id = dictEquipment.Id }, dictEquipment);

            await _repository.PostDictEquipment(dictEquipment); 
            return CreatedAtAction(nameof(PostDictEquipment), new { id = dictEquipment.Id }, dictEquipment);
        }

    }
}
//// POST: api/DictEquipments
//[HttpPost]
//public async Task<ActionResult<DictEquipment>> PostDictEquipment(DictEquipment dictEquipment)
//{
//    _context.DictEquipments.Add(dictEquipment);
//    await _context.SaveChangesAsync();

//    return CreatedAtAction(nameof(PostDictEquipment), new { id = dictEquipment.Id }, dictEquipment);
//}