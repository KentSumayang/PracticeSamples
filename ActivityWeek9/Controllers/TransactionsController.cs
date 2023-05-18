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
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get_Transactions()
        {
          if (_context._Transactions == null)
          {
              return NotFound();
          }
            return await _context._Transactions
                .Include(x => x.User)
                .ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction(int id)
        {
          if (_context._Transactions == null)
          {
              return NotFound();
          }
            var transaction = await _context._Transactions
                .Where(x => x.User.Id == id)
                .ToListAsync();

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            if (_context._Transactions == null)
            {
                return NotFound();
            }
            var transaction = await _context._Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context._Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("Deposit")]
        public async Task<IActionResult> DepositTransaction(int id, int amount)
        {
            try
            {
                var account = await _context._Users.FirstOrDefaultAsync(x => x.Id == id);             
                if (account == null)
                {
                    return BadRequest("Account not Found");
                }

                var balance = account.Balance;
                var new_balance = balance + amount;
                account.Balance = new_balance;

                Transaction transaction = new Transaction()
                {
                    User = account,
                    Amount = amount,
                    Type = "Deposit",
                    Timestamp = DateTime.UtcNow
                };
                _context._Transactions.Add(transaction);


                Notification notification = new Notification()
                {
                    User = account,
                    Transaction = transaction,
                    Message =
                        "Current Balance : " + balance + ".\n" + 
                        account.Username + " has deposited: " + amount + " amount to account number: " + account.Id + ".\n" +
                        "New Balance: " + new_balance,
                    IsRead = false
                };
                _context._Notifications.Add(notification);

                await _context.SaveChangesAsync();

                return Ok(transaction);
            }
            catch (Exception)
            {

                return BadRequest("Server Error.");
            }
        }

        [HttpPatch("Withdraw")]
        public async Task<IActionResult> WithdrawTransaction(int id, int amount)
        {
            try
            {
                var account = await _context._Users.FirstOrDefaultAsync(x => x.Id == id);
                if (account == null)
                {
                    return BadRequest("Account not Found");
                }

                var balance = account.Balance;
                var new_balance = balance - amount;
                account.Balance = new_balance;

                Transaction transaction = new Transaction()
                {
                    User = account,
                    Amount = amount,
                    Type = "Withdraw",
                    Timestamp = DateTime.UtcNow
                };
                _context._Transactions.Add(transaction);

                Notification notification = new Notification()
                {
                    User = account,
                    Transaction = transaction,
                    Message =
                        "Current Balance : " + balance + ".\n" +
                        account.Username + " has withdrawn: " + amount + " amount to account number: " + account.Id + ".\n" +
                        "New Balance: " + new_balance,
                    IsRead = false
                };
                _context._Notifications.Add(notification);

                await _context.SaveChangesAsync();

                return Ok(transaction);
            }
            catch (Exception)
            {
                return BadRequest("Server Error.");
            }

        }
    }
}
