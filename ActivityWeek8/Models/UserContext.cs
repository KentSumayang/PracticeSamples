using Microsoft.EntityFrameworkCore;

namespace ActivityWeek8.Models
{
    public class UserContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public UserContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = _configuration.GetConnectionString("UserCS");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));
            

            optionsBuilder.UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure());
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserInfo>().Ignore(x => x.FirstName).Ignore(x=> x.LastName);

        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserOtherInfo> UserOtherInfos { get; set; }
    }
}
