using Microsoft.EntityFrameworkCore;

namespace PortfolioApi.Interface
{
    public interface DbContext
    {
        DbSet<Context> DbContexts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

