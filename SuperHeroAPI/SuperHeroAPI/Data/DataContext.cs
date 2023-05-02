using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("UserCS"));
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
