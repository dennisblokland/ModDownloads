using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModDownloads.Server.Context;
using ModDownloads.Shared.Entities;

namespace ModDownloads.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModsController : ControllerBase
    {
        private readonly DownloadsContext _context;

        public ModsController(DownloadsContext context)
        {
            _context = context;
        }

        // GET: api/Mods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mod>>> GetMod()
        {
            return await _context.Mod.ToListAsync();
        }

        // GET: api/Mods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mod>> GetMod(int id)
        {
            var mod = await _context.Mod.FindAsync(id);

            if (mod == null)
            {
                return NotFound();
            }

            return mod;
        }
    
        [HttpGet("byName/{name}")]
        public async Task<ActionResult<Mod>> GetMod(string Name)
        {
            var mod = await _context.Mod.Where(m => m.Name == Name).FirstAsync();

            if (mod == null)
            {
                return NotFound();
            }

            return mod;
        }
        [HttpGet("{id}/downloads")]
        public async Task<ActionResult<IEnumerable<Download>>> GetModDownloads(int id)
        {
            return await _context.Download.Where(x => x.ModId == id).OrderBy(d => d.Timestamp).ToListAsync();
        }

        [HttpGet("{id}/downloads/increase")]
        public Dictionary<DateTime, int> GetModDownloadsIncrease(int id)
        {
            Dictionary<DateTime, int> dict = new Dictionary<DateTime, int>();
            List<Download> downloads = _context.Download.Where(x => x.ModId == id).OrderBy(d => d.Timestamp).ToList();
            for (int key = 0; key < downloads.Count; ++key)
            {
                if(key != 0)
                {
                    dict.Add(downloads[key].Timestamp, downloads[key].Downloads - downloads[key - 1].Downloads);
                }
            }
            return dict;
        }
        [HttpGet("{id}/downloads/byDate")]
        public async Task<ActionResult<IEnumerable<Download>>> GetDownloadByDate(int id, DateTime startTime, DateTime endtime)
        {
            return await _context.Download.Where(d => d.ModId == id && d.Timestamp >= startTime && d.Timestamp <= endtime).OrderBy(d => d.Timestamp).ToListAsync();
        }
        // PUT: api/Mods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMod(int id, Mod mod)
        {
            if (id != mod.ID)
            {
                return BadRequest();
            }

            _context.Entry(mod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModExists(id))
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

        // POST: api/Mods
        [HttpPost]
        public async Task<ActionResult<Mod>> PostMod(Mod mod)
        {
            _context.Mod.Add(mod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMod", new { id = mod.ID }, mod);
        }

        // DELETE: api/Mods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Mod>> DeleteMod(int id)
        {
            var mod = await _context.Mod.FindAsync(id);
            if (mod == null)
            {
                return NotFound();
            }

            _context.Mod.Remove(mod);
            await _context.SaveChangesAsync();

            return mod;
        }

        private bool ModExists(int id)
        {
            return _context.Mod.Any(e => e.ID == id);
        }
    }
}
