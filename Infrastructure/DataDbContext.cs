using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PortfolioApi.Models;

namespace DotNetApi.Infrastructure
{
    public class DataDbContext : DbContext
    {
        public DbSet<PostItem> Posts { get; set; }
        public DataDbContext(DbContextOptions<DataDbContext> options) : base() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
