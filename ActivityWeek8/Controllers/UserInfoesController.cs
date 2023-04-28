using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActivityWeek8.Models;

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
            var properties = await _context.UserInfos.Include(a => a.UserOtherInfo).ToListAsync();
            return properties;
        }

        // PUT: api/UserInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("EditUserOtherInfo")]
        public async Task<IActionResult> PutUserInfo(UserInfo userInfo)
        {

            try
            {
                _context.Entry(userInfo).State = EntityState.Modified;
                _context.Entry(userInfo).Property(x => x.Id).IsModified = false;
                _context.Entry(userInfo.UserOtherInfo).Property(x => x.Id).IsModified = false;
                _context.Entry(userInfo).Property(x => x.LastName).IsModified = false;
                _context.Entry(userInfo).Property(x => x.FirstName).IsModified = false;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoExists(userInfo.Id))
                {
                    return NotFound();
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
          if (_context.UserInfos == null)
          {
              return Problem("Entity set 'UserContext.UserInfos'  is null.");
          }
            await _context.UserInfos.AddAsync(userInfo);
            userInfo.Status = 1;
            await _context.SaveChangesAsync();

            return Ok(userInfo);
        }
        [HttpPost("GetInfoViaUsernameAndPassword")]
        public async Task<ActionResult<UserInfo>> GetInformation (string username, string password)
        {
            var exist_username = await _context.UserInfos.Where(x => x.Username == username).FirstOrDefaultAsync();
            var exist_password = await _context.UserInfos.Where(x => x.Password == password).FirstOrDefaultAsync();
            var properties = await _context.UserInfos.Include(a => a.UserOtherInfo)
                .Where(x => x.Username == username).ToListAsync();


            if (exist_username != null)
            {
                if (exist_password != null)
                {
                    if (exist_password.Status == 0)
                    {
                        return BadRequest("Invalid. Account has been Deactivated.");
                    }
                    return Ok(properties);
                }
                return BadRequest("Username or Password is Invalid");
            }
            return BadRequest("Username or Password is Invalid");
        }

        [HttpPost("DeactivateUser")]
        public async Task<ActionResult<UserInfo>> SetUserStatusDeactive(string username, string password)
        {
            var exist_username = await _context.UserInfos.Where(x => x.Username == username).FirstOrDefaultAsync();
            var exist_password = await _context.UserInfos.Where(x => x.Password == password).FirstOrDefaultAsync();
            
            if (exist_username != null)
            {
                if (exist_password != null)
                {
                    if (exist_username.Status == 0)
                    {
                        return Ok("Account is already inactive.");
                    }
                    exist_username.Status = 0;
                    await _context.SaveChangesAsync();

                    return Ok("Account has been deactivated.");
                }
                return BadRequest("Username or Password is Invalid");
            }
            return BadRequest("Username or Password is Invalid");
        }
        [HttpPost("ReactivateUser")]
        public async Task<ActionResult<UserInfo>> SetUserStatusActive(string username, string password)
        {
            var exist_username = await _context.UserInfos.Where(x => x.Username == username).FirstOrDefaultAsync();
            var exist_password = await _context.UserInfos.Where(x => x.Password == password).FirstOrDefaultAsync();
            
            if (exist_username != null)
            {
                if (exist_password != null)
                {
                    if (exist_username.Status == 1)
                    {
                        return Ok("Account is already active.");
                    }
                    exist_username.Status = 1;
                    await _context.SaveChangesAsync();

                    return Ok("Account has been reactivated.");
                }
                return BadRequest("Username or Password is Invalid");
            }
            return BadRequest("Username or Password is Invalid");
        }
        private bool UserInfoExists(int id)
        {
            return (_context.UserInfos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
