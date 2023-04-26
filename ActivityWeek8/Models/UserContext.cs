using Microsoft.EntityFrameworkCore;

namespace ActivityWeek8.Models
{
    public class UserContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = "server=10.0.11.141; port=3306; user=devusr; password=a2j?XU4^dU?6DmN@; database=SumayangDB";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));
            

            optionsBuilder.UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure());
        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserOtherInfo> UserOtherInfos { get; set; }
    }
}
