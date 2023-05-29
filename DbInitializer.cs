using Microsoft.EntityFrameworkCore;
using PortfolioApi.DBContext;
namespace PortfolioApi
{
    public class DbInitializer
    {
        public static void Initialize(DbContextPost context)
        {
            context.Database.Migrate();
        } 
    }
}
