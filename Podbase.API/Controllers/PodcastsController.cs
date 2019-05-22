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
    public class PodcastsController : ControllerBase
    {
        private readonly PodbaseContext _context;

        public PodcastsController(PodbaseContext context)
        {
            _context = context;
        }

        // GET: api/Podcasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Podcast>>> GetPodcasts()
        {
            return await _context.Podcasts.ToListAsync();
        }

        // GET: api/Podcasts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPodcast([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var podcast = await _context.Podcasts.FindAsync(id);

            if (podcast == null)
            {
                return NotFound();
            }

            return Ok(podcast);
        }

        // PUT: api/Podcasts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPodcast([FromRoute] int id, [FromBody] Podcast podcast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != podcast.PodcastId)
            {
                return BadRequest();
            }

            _context.Entry(podcast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PodcastExists(id))
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

        // POST: api/Podcasts
        [HttpPost]
        public async Task<IActionResult> PostPodcast([FromBody] Podcast podcast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Podcasts.Add(podcast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPodcast", new { id = podcast.PodcastId }, podcast);
        }

        // DELETE: api/Podcasts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePodcast([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var podcast = await _context.Podcasts.FindAsync(id);
            if (podcast == null)
            {
                return NotFound();
            }

            _context.Podcasts.Remove(podcast);
            await _context.SaveChangesAsync();

            return Ok(podcast);
        }

        private bool PodcastExists(int id)
        {
            return _context.Podcasts.Any(e => e.PodcastId == id);
        }
    }
}