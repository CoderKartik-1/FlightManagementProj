using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightApi.Models;
using FlightApi.Service;

namespace FlightApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserSer<Kguser> _kgSer;

        public UserController(IUserSer<Kguser> kgSer)
        {
            _kgSer = kgSer;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kguser>>> GetKgusers()
        {
            return _kgSer.GetAllUsers();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kguser>> GetKguser(int id)
        {
            var kguser = _kgSer.GetUserById(id);

            if (kguser == null)
            {
                return NotFound();
            }

            return kguser;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKguser(int id, Kguser kguser)
        {
            Console.WriteLine($"{kguser.Ulocation} {kguser.Uid} {id}");
            if (id != kguser.Uid)
            {
                return BadRequest();
            }

            // _context.Entry(kguser).State = EntityState.Modified;

            try
            {
                _kgSer.UpdateUser(id, kguser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KguserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kguser>> PostKguser(Kguser kguser)
        {
            try
            {
                // await _context.SaveChangesAsync();
                _kgSer.AddUser(kguser);
            }
            catch (DbUpdateException)
            {
                if (KguserExists(kguser.Uid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKguser", new { id = kguser.Uid }, kguser);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKguser(int id)
        {
            var kguser = _kgSer.GetUserById(id);
            if (kguser == null)
            {
                return NotFound();
            }

            _kgSer.DeleteUser(id);

            return NoContent();
        }

        private bool KguserExists(int id)
        {
            Kguser a= _kgSer.GetUserById(id);
            if(a!=null){
                return true;
            }
            else{
                return false;
            }
            // return _context.Kgusers.Any(e => e.Uid == id);
        }
    }
}
