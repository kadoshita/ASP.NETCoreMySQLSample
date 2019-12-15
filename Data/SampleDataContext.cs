using Microsoft.EntityFrameworkCore;

namespace SampleDataContext.Data
{
    public class SampleDataContext : DbContext
    {
        public SampleDataContext(DbContextOptions<SampleDataContext> options) : base(options)
        {

        }
        public DbSet<ASP.NETCoreMySQLSample.Models.SampleData> SampleData { get; set; }
    }
}