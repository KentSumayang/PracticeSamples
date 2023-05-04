using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DataContext (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = _configuration.GetConnectionString("SuperheroCS");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));


            optionsBuilder.UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure());
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
