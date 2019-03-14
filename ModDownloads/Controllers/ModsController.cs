using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModDownloads.Server.Context;
using ModDownloads.Server.Enum;
using ModDownloads.Shared.Entities;

namespace ModDownloads.Server.Controllers
{
    [Authorize]
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

            return mod == null ? (ActionResult<Mod>)NotFound() : (ActionResult<Mod>)mod;
        }

        [HttpGet("byName/{name}")]
        public async Task<ActionResult<Mod>> GetMod(string Name)
        {
            var mod = await _context.Mod.Where(m => m.Name == Name).FirstAsync();

            return mod == null ? (ActionResult<Mod>)NotFound() : (ActionResult<Mod>)mod;
        }

        [HttpGet("{id}/downloads")]
        public async Task<ActionResult<IEnumerable<Download>>> GetModDownloads(int id)
        {
            return await GetDownloadByDate(id, new DateTime(), Grouping.Day);

        }

        [HttpGet("{id}/downloads/increase")]
        public Dictionary<DateTime, int> GetModDownloadsIncrease(int id)
        {
            List<Download> downloads = (List<Download>)this.GetDownloadByDate(id, new DateTime(), Grouping.Day).Result.Value;

            return DownloadsHelper.GetDownloadsIncrease(downloads);
        }

        [HttpGet("{id}/downloads/byDate")]
        public async Task<ActionResult<IEnumerable<Download>>> GetDownloadByDate(int id, DateTime startTime, Grouping grouping)
        {
            if(grouping == Grouping.None)
            {
                return await _context.Download
                .Where(d => d.ModId == id && d.Timestamp >= startTime)
                .OrderBy(d => d.Timestamp).ToListAsync().ConfigureAwait(false);
            }
            else
            {
                return await _context.Download
                .Where(d => d.ModId == id && d.Timestamp >= startTime)
                .GroupBy(x => getGroupByFromGrouping(grouping, x))
                .Select(x => new Download
                {
                    Id = x.Select(y => y.Id).First(),
                    Downloads = x.Max(y => y.Downloads),
                    Timestamp = x.Max(y => y.Timestamp),
                    ModId = x.Select(y => y.ModId).First()

                })
                .OrderBy(d => d.Timestamp).ToListAsync().ConfigureAwait(false);
            }

        }

        private object getGroupByFromGrouping(Grouping groupingEnum, Download x)
        {
            object grouping = null;
            switch (groupingEnum)
            {
                case Grouping.None:
                    break;
                case Grouping.Day:
                    grouping = new { x.Timestamp.Year, x.Timestamp.Month, x.Timestamp.Day };
                    break;
                case Grouping.Month:
                    grouping = new { x.Timestamp.Year, x.Timestamp.Month};
                    break;
                case Grouping.Year:
                    grouping = new { x.Timestamp.Year};
                    break;
                default:
                    break;
            }
           

            return grouping;


        }

        [HttpGet("{id}/downloads/Daily")]
        public int GetTotalDownloadsDaily(int id)
        {
            double count = 0.0;
            DateTime date = DateTime.Now;

            DateTime startOfDay = new DateTime(date.Year, date.Month, date.Day, 00 ,00 ,00);
            List<Download> downloads = _context.Download.Where(x => x.ModId == id && x.Timestamp >= startOfDay && x.Timestamp <= startOfDay.AddDays(1)).OrderByDescending(x => x.Timestamp).ToList();
            if (downloads.Count != 0)
            {
                count += downloads[0].Downloads - downloads.Last().Downloads;
            }
           
            return (int)Math.Round(count);
        }

        [HttpGet("{id}/downloads/Monthly")]
        public int GetTotalDownloadsMonthly(int id)
        {
            double count = 0.0;
            DateTime date = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            List<Download> downloads = _context.Download.Where(x => x.ModId == id && x.Timestamp >= firstDayOfMonth && x.Timestamp <= DateTime.Now).OrderByDescending(x => x.Timestamp).ToList();
            if (downloads.Count != 0)
            {
                count += downloads[0].Downloads - downloads.Last().Downloads;
            }

            return (int)Math.Round(count);
        }
        [HttpGet("{id}/downloads/Months")]
        public Dictionary<int,int> GetTotalDownloadsAllMonths(int id)
        {
            Dictionary<int,int> dict = new Dictionary<int, int>();
            for (int i = 1; i <= DateTime.Now.Month; i++)
            {
                double count = 0.0;
                DateTime date = DateTime.Now;
                DateTime firstDayOfMonth = new DateTime(date.Year, i, 1);
                DateTime lastDayOfMonth = new DateTime(date.Year, i, DateTime.DaysInMonth(DateTime.Now.Year, i)).AddDays(1);

                List<Download> downloads = _context.Download.Where(x => x.ModId == id && x.Timestamp >= firstDayOfMonth && x.Timestamp <= lastDayOfMonth).OrderByDescending(x => x.Timestamp).ToList();
                if (downloads.Count != 0)
                {
                    count += downloads[0].Downloads - downloads.Last().Downloads;
                }
                dict.Add(i, (int)Math.Round(count));
            }
   

            return dict;
        }

        [HttpGet("{id}/downloads/Yearly")]
        public int GetTotalDownloadsYearly(int id)
        {
            double count = 0.0;
            DateTime date = DateTime.Now;
            DateTime firstDayOfYear = new DateTime(date.Year, 1, 1);
            List<Download> downloads = _context.Download.Where(x => x.ModId == id && x.Timestamp >= firstDayOfYear && x.Timestamp <= DateTime.Now).OrderByDescending(x => x.Timestamp).ToList();
      
            if (downloads.Count != 0)
            {
                count += downloads[0].Downloads - downloads.Last().Downloads;

            }
            return (int)Math.Round(count);
        }

        [HttpGet("{id}/downloads/Total")]
        public int GetTotalDownloads(int id)
        {
            Download download = _context.Download.Where(x => x.ModId == id).OrderByDescending(x => x.Timestamp).FirstOrDefault();
            if (download != null)
            {
                return download.Downloads;

            }
            
            return 0;
        }

        // PUT: api/Mods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMod(int id, Mod mod)
        {
            if (id != mod.Id)
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

            return CreatedAtAction("GetMod", new { id = mod.Id }, mod);
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
            return _context.Mod.Any(e => e.Id == id);
        }
    }
}
