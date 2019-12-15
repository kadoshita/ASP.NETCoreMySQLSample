using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreMySQLSample.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<ASP.NETCoreMySQLSample.Models.User> User { get; set; }
    }
}