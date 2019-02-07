﻿using System;
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