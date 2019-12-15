using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreMySQLSample.Data
{
    public class SampleDataContext : DbContext
    {
        public SampleDataContext(DbContextOptions<SampleDataContext> options) : base(options)
        {

        }
        public DbSet<ASP.NETCoreMySQLSample.Models.SampleData> SampleData { get; set; }
    }
}