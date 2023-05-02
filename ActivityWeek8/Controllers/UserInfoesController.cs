using ActivityWeek8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ActivityWeek8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoesController : ControllerBase
    {
        private readonly UserContext _context;


        public UserInfoesController(UserContext context)
        {
            _context = context;
        }

        [HttpGet("GetUserInfo")]
        public async Task<ActionResult<IEnumerable<UserInfo>>> Get()
        {
            if (_context.UserInfos == null)
            {
                return NotFound("Data Not Available");
            }
            var properties = await _context.UserInfos
                .Include(a => a.UserOtherInfo)
                .ToListAsync();
            return properties;
        }

        // PUT: api/UserInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("EditUserInfo")]
        public async Task<IActionResult> PutUserInfo(UserInfo userInfo)
        {

            try
            {
                _context.Entry(userInfo).State = EntityState.Modified;
                _context.Entry(userInfo).Property(x => x.Id).IsModified = false;
                _context.Entry(userInfo).Property(x => x.LastName).IsModified = false;
                _context.Entry(userInfo).Property(x => x.FirstName).IsModified = false;
                _context.Entry(userInfo.UserOtherInfo).Property(x => x.Id).IsModified = false;
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoExists(userInfo.Id))
                {
                    return NotFound("User Not Found.");
                }
                else
                {
                    throw;
                }
            }

            return Ok(userInfo);
        }


        // POST: api/UserInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserInfo>> PostUserInfo(UserInfo userInfo)
        {
            if (_context.UserInfos != null)
            {
                await _context.UserInfos.AddAsync(userInfo);
                userInfo.Status = 1;
                await _context.SaveChangesAsync();

                return Ok("New Account is Created.");
            }
            return Problem("Table is empty. No data available");
            
        }

        [HttpPost("GetInfoViaUsernameAndPassword")]
        public async Task<ActionResult<UserInfo>> GetInformation(string username, string password)
        {
            if (_context.UserInfos != null)
            {
                var exist_user = await _context.UserInfos
                .Where(x => x.Username == username)
                .Where(x => x.Password == password)
                .Include(a => a.UserOtherInfo)
                .FirstOrDefaultAsync();

                if (exist_user != null)
                {
                    if (exist_user != null)
                    {
                        if (exist_user.Status == 0)
                        {
                            return Ok("Invalid. Account has been Deactivated.");
                        }
                        return Ok(exist_user);
                    }
                    return BadRequest("Username or Password is Invalid");
                }
                return BadRequest("Username or Password is Invalid");
            }
            return NotFound();

            
        }

        [HttpPost("DeactivateUser")]
        public async Task<ActionResult<UserInfo>> SetUserStatusDeactive(string username, string password)
        {
            

            if (_context.UserInfos != null)
            {
                var exist_user = await _context.UserInfos
                .Where(x => x.Username == username)
                .Where(x => x.Password == password)
                .FirstOrDefaultAsync();

                if (exist_user != null)
                {
                    if (exist_user != null)
                    {
                        if (exist_user.Status == 0)
                        {
                            return Ok("Account is already inactive.");
                        }
                        exist_user.Status = 0;
                        await _context.SaveChangesAsync();

                        return Ok("Account has been deactivated.");
                    }
                    return BadRequest("Username or Password is Invalid");
                }
                return BadRequest("Username or Password is Invalid");
            }
            return NotFound();
            
        }

        [HttpPost("ReactivateUser")]
        public async Task<ActionResult<UserInfo>> SetUserStatusActive(string username, string password)
        {
            

            if (_context.UserInfos != null)
            {
                var exist_user = await _context.UserInfos
                .Where(x => x.Username == username)
                .Where(x => x.Password == password)
                .FirstOrDefaultAsync();

                if (exist_user != null)
                {
                    if (exist_user != null)
                    {
                        if (exist_user.Status == 1)
                        {
                            return Ok("Account is already active.");
                        }
                        exist_user.Status = 1;
                        await _context.SaveChangesAsync();

                        return Ok("Account has been reactivated.");
                    }
                    return BadRequest("Username or Password is Invalid");
                }
                return BadRequest("Username or Password is Invalid");
            }
            return NotFound();
        }

           
        private bool UserInfoExists(int id)
        {
            return (_context.UserInfos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("DeleteUserInfo")]
        public async Task<ActionResult<UserInfo>> DeleteUserInfo(string username, string password)
        {
            try
            {
                if (_context.UserInfos != null)
                {
                    if (username != null || password != null)
                    {
                        var exist_user = await _context.UserInfos
                       .Where(x => x.Username == username)
                       .Where(x => x.Password == password)
                       .FirstOrDefaultAsync();

                        _context.UserInfos.Remove(exist_user);

                        await _context.SaveChangesAsync();

                        return Ok("User" + exist_user.Username + " has been deleted.");
                    }
                    return NotFound();
                }
                return NotFound();  
            }
            catch (Exception)
            {

                return NotFound("User not found.");
            }
            
        }
    }
    
}
