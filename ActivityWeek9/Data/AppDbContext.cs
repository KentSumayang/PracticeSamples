using ActivityWeek9.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ActivityWeek9.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = _configuration.GetConnectionString("UserCS");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));


            optionsBuilder.UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure());
        }
        public DbSet<User> _Users { get; set; }
        public DbSet<Transaction> _Transactions { get; set; }
        public DbSet<Notification> _Notifications { get; set; }
    }
}
