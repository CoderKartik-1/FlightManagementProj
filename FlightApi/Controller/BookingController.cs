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
    public class BookingController : ControllerBase
    {
        private readonly Ace52024Context _context;

        public BookingController(Ace52024Context context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kgbooking>>> GetKgbookings()
        {
            return await _context.Kgbookings.ToListAsync();
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kgbooking>> GetKgbooking(int id)
        {
            var kgbooking = await _context.Kgbookings.FindAsync(id);

            if (kgbooking == null)
            {
                return NotFound();
            }

            return kgbooking;
        }

        // PUT: api/Booking/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKgbooking(int id, Kgbooking kgbooking)
        {
            if (id != kgbooking.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(kgbooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KgbookingExists(id))
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

        // POST: api/Booking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kgbooking>> PostKgbooking(Kgbooking kgbooking)
        {
            _context.Kgbookings.Add(kgbooking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKgbooking", new { id = kgbooking.BookingId }, kgbooking);
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKgbooking(int id)
        {
            var kgbooking = await _context.Kgbookings.FindAsync(id);
            if (kgbooking == null)
            {
                return NotFound();
            }

            _context.Kgbookings.Remove(kgbooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KgbookingExists(int id)
        {
            return _context.Kgbookings.Any(e => e.BookingId == id);
        }
    }
}
