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
            //return await _context.DictPriorities.ToListAsync();
            return await _repository.GetDictPriorities();

        }

        // GET: api/DictPriorities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DictPriority>> GetDictPriority(int id)
        {
            //var dictPriority = await _context.DictPriorities.FindAsync(id);
            var dictPriority = await _repository.GetDictPriority(id);

            if (dictPriority == null)
            {
                return NotFound();
            }

            return dictPriority;
        }

        //private bool DictPriorityExists(int id)
        //{
        //    return _context.DictPriorities.Any(e => e.Id == id);
        //}
    }
}
