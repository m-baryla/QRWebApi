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
            //return await _context.DictStatus.ToListAsync();
            return await _repository.GetDictStatus();
        }

        //private bool DictStatuExists(int id)
        //{
        //    return _context.DictStatus.Any(e => e.Id == id);
        //}
    }
}
