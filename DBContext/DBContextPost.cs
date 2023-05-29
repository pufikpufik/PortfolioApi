using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortfolioApi.Interface;
using PortfolioApi.Models;

namespace PortfolioApi.DBContext
{
    public class DbContextPost : DbContext, IDbContextPost
    {
        public DbSet<PostItem> Posts { get; set; }
        public DbContextPost(DbContextOptions<DbContextPost> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
        base.OnConfiguring(optionsBuilder);
        var connectionString = "Host=localhost;Database=portfolio_edu;Usernane=postgres;Password=1";
        optionsBuilder.UseNpgsql(connectionString);

            }
    }
}
