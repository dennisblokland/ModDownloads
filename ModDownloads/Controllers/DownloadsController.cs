using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModDownloads.Server.Context;
using ModDownloads.Shared.Entities;

namespace ModDownloads.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadsController : ControllerBase
    {
        private readonly DownloadsContext _context;

        public DownloadsController(DownloadsContext context)
        {
            _context = context;
        }

        // GET: api/Downloads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Download>>> GetDownload()
        {
            return await _context.Download.OrderBy(d => d.Timestamp).ToListAsync();
        }
        [HttpGet("byDate")]
        public async Task<ActionResult<IEnumerable<Download>>> GetDownload(DateTime startTime, DateTime endtime)
        {
            return await _context.Download.Where(d => d.Timestamp >= startTime && d.Timestamp <= endtime).OrderBy(d => d.Timestamp).ToListAsync();
        }
        [HttpGet("Daily")]
        public int GetTotalDownloadsDaily()
        {
            double count = 0.0;
            foreach (Mod mod in _context.Mod)
            {
                count += new ModsController(_context).GetTotalDownloadsDaily(mod.Id);
            }
            return (int)Math.Round(count);
        }
        [HttpGet("Monthly")]
        public int GetTotalDownloadsMonthly()
        {
            double count = 0.0;
            foreach (Mod mod in _context.Mod)
            {
                count += new ModsController(_context).GetTotalDownloadsMonthly(mod.Id);
            }
            return (int)Math.Round(count);
        }

        [HttpGet("Yearly")]
        public int GetTotalDownloadsYearly()
        {
            double count = 0.0;
            foreach (Mod mod in _context.Mod)
            {
                count += new ModsController(_context).GetTotalDownloadsYearly(mod.Id);
            }
            return (int)Math.Round(count);
        }

        [HttpGet("Total")]
        public int GetTotalDownloads()
        {
            int count = 0;
            foreach(Mod mod in _context.Mod)
            {
                Download download = _context.Download.Where(x => x.ModId == mod.Id).OrderByDescending(x => x.Timestamp).FirstOrDefault();
                if(download != null)
                {
                    count += download.Downloads;

                }
            }
            return count;
        }
        // GET: api/Downloads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Download>> GetDownload(int id)
        {
            var download = await _context.Download.FindAsync(id);

            if (download == null)
            {
                return NotFound();
            }

            return download;
        }

        // PUT: api/Downloads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDownload(int id, Download download)
        {
            if (id != download.Id)
            {
                return BadRequest();
            }

            _context.Entry(download).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DownloadExists(id))
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

        // POST: api/Downloads
        [HttpPost]
        public async Task<ActionResult<Download>> PostDownload(Download download)
        {
            _context.Download.Add(download);
           
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDownload", new { id = download.Id }, download);
        }

        // DELETE: api/Downloads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Download>> DeleteDownload(int id)
        {
            var download = await _context.Download.FindAsync(id);
            if (download == null)
            {
                return NotFound();
            }

            _context.Download.Remove(download);
            await _context.SaveChangesAsync();

            return download;
        }

        private bool DownloadExists(int id)
        {
            return _context.Download.Any(e => e.Id == id);
        }
    }
}
