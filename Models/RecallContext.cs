using Microsoft.EntityFrameworkCore;


namespace PortfolioApi.Models;

public class RecallContext : DbContext
{
    public RecallContext(DbContextOptions<RecallContext> options)
        : base(options)
    {
    }

    public DbSet<RecallItem> RecallItems { get; set; } = null!;

}