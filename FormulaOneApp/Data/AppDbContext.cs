using Microsoft.EntityFrameworkCore;
using FormulaOneApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FormulaOneApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Teams> Teams { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}
