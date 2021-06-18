using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QRWebApi.Models;

namespace QRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WikisController : ControllerBase
    {
        private readonly Repository _repository;

        public WikisController(QRAppDBContext context)
        {
            _repository = new Repository(context);
        }

        // GET: api/Wikis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wiki>>> GetWikis()
        {
            return await _repository.GetWikis();
        }

        // GET: /api/Wikis/GetWikiDetail/
        [HttpGet("GetWikiDetail/")]
        public async Task<ActionResult<IEnumerable<WikiDetails>>> GetWikiDetail()
        {
            return await _repository.GetWikiDetail();
        }

        // POST: api/Wikis
        [HttpPost]
        public async Task<ActionResult<Wiki>> PostWiki(WikiDetails wiki)
        {
            if (wiki != null)
            {
                 await _repository.PostWiki(wiki);

                 return CreatedAtAction(nameof(PostWiki), new { id = wiki.Id }, wiki);
            }
            return NotFound();
        }
    }
}