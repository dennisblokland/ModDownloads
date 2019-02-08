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
            if (id != download.ID)
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

            return CreatedAtAction("GetDownload", new { id = download.ID }, download);
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
            return _context.Download.Any(e => e.ID == id);
        }
    }
}
