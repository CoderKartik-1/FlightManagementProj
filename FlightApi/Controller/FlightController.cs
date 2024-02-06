using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightApi.Models;

namespace FlightApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly Ace52024Context _context;

        public FlightController(Ace52024Context context)
        {
            _context = context;
        }

        // GET: api/Flight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kgflight>>> GetKgflights()
        {
            return await _context.Kgflights.ToListAsync();
        }

        // GET: api/Flight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kgflight>> GetKgflight(int id)
        {
            var kgflight = await _context.Kgflights.FindAsync(id);

            if (kgflight == null)
            {
                return NotFound();
            }

            return kgflight;
        }

        // PUT: api/Flight/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKgflight(int id, Kgflight kgflight)
        {
            Console.WriteLine($"{kgflight.Frate} {kgflight.Fid}");
            if (id != kgflight.Fid)
            {
                return BadRequest();
            }

            _context.Entry(kgflight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KgflightExists(id))
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

        // POST: api/Flight
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kgflight>> PostKgflight(Kgflight kgflight)
        {
            _context.Kgflights.Add(kgflight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKgflight", new { id = kgflight.Fid }, kgflight);
        }

        // DELETE: api/Flight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKgflight(int id)
        {
            var kgflight = await _context.Kgflights.FindAsync(id);
            if (kgflight == null)
            {
                return NotFound();
            }

            _context.Kgflights.Remove(kgflight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KgflightExists(int id)
        {
            return _context.Kgflights.Any(e => e.Fid == id);
        }
    }
}
