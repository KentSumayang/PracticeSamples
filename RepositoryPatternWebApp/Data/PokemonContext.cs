using Microsoft.EntityFrameworkCore;
using RepositoryPatternWebApp.Models;

namespace RepositoryPatternWebApp.Data
{
    public class PokemonContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public PokemonContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = _configuration.GetConnectionString("UserCS");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));


            optionsBuilder.UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure());
        }
        public DbSet<Pokemon> Pokemons { get; set; }
    }
}
