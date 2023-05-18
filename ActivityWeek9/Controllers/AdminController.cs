using ActivityWeek9.Data;
using ActivityWeek9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActivityWeek9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetAllNotifications()
        {
            var result = await _context._Notifications
                    .ToListAsync();

            if (_context._Notifications != null)
            {
                return result;
            }
            return NotFound("No Data Found.");

        }
        [HttpGet("ViewNotifications")]
        public async Task<ActionResult<IEnumerable<Notification>>> ViewNotifications(int id, string username, string password)
        {
            var user = await _context._Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound("User not found.");
            }
            if (user.IsAdmin == false)
            {
                return BadRequest("Invalid. For administrators only.");
            }
            if (user.Username != username || user.Password != password)
            {
                return BadRequest("Incorrect Username or Password");
            }

            var result = await _context._Notifications
                    .Where(x => x.IsRead == false)
                    .Include(x => x.User)
                    .Include(x => x.Transaction)
                    .ToListAsync();

            if (result == null)
            {
                return NotFound("No Data Found.");
            }

            await _context._Notifications
                .Where(x => x.IsRead == false)
                .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsRead, true));

            await _context.SaveChangesAsync();

            return result;

        }
        [HttpGet("GetNotifications")]
        public async Task<ActionResult<Notification>> GetNotification()
        {
            var count = await _context._Notifications.CountAsync(x => x.IsRead == false);
            return Ok(count + " unread notification(s).");
        }
    }
}
