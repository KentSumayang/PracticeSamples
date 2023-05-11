using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActivityWeek9.Data;
using ActivityWeek9.Models;

namespace ActivityWeek9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get_Users()
        {
            var result = await _context._Users
                    .ToListAsync();

            if (_context._Users != null)
            {
                return result;
            }
            return NotFound("No Data Found.");

        }

        // GET: api/Users/5
       

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context._Users == null)
          {
              return Problem("Entity set 'AppDbContext._Users'  is null.");
          }
            user.IsAdmin = false;

            _context._Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context._Users == null)
            {
                return NotFound();
            }
            var user = await _context._Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context._Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context._Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool IsRead ()
        {
            var notification =_context._Notifications
                .FirstOrDefaultAsync(x=>x.IsRead == false);
            
            return true;
        }
    }
}
