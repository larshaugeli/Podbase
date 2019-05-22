using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Podbase.DataAccess;
using Podbase.Model;

namespace Podbase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountPodcastsController : ControllerBase
    {
        private readonly PodbaseContext _context;

        public AccountPodcastsController(PodbaseContext context)
        {
            _context = context;
        }
        
        // GET: api/AccountPodcasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountPodcast>>> GetAccountPodcasts()
        {
            return await _context.AccountPodcasts.ToListAsync();
        }

        // GET: api/AccountPodcasts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountPodcast([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountPodcast = await _context.AccountPodcasts.FindAsync(id);

            if (accountPodcast == null)
            {
                return NotFound();
            }

            return Ok(accountPodcast);
        }

        // PUT: api/AccountPodcasts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountPodcast([FromRoute] int id, [FromBody] AccountPodcast accountPodcast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accountPodcast.PodcastId)
            {
                return BadRequest();
            }

            _context.Entry(accountPodcast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountPodcastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AccountPodcasts
        [HttpPost]
        public async Task<IActionResult> PostAccountPodcast([FromBody] AccountPodcast accountPodcast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AccountPodcasts.Add(accountPodcast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountPodcast", new { id = accountPodcast.PodcastId }, accountPodcast);
        }

        // DELETE: api/AccountPodcasts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountPodcast([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountPodcast = await _context.AccountPodcasts.FindAsync(id);
            if (accountPodcast == null)
            {
                return NotFound();
            }

            _context.AccountPodcasts.Remove(accountPodcast);
            await _context.SaveChangesAsync();

            return Ok(accountPodcast);
        }

        private bool AccountPodcastExists(int id)
        {
            return _context.AccountPodcasts.Any(e => e.PodcastId == id);
        }
    }
}