using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.DBContext;
using PortfolioApi.Interface;

namespace PortfolioApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = string.Empty;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVITONMENT") == "Development")
            {
                connectionString = configuration["DbConnection"];
            }
            else
            {
                connectionString = GetConnectionString();
            }
            services.AddDbContext<DbContextPost>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IDbContextPost>(provider =>
            provider.GetService<DbContextPost>());
             
            return services;
        }
        public static string GetConnectionString()
        {
            var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            connectionUrl = connectionUrl.Replace("postgres://", string.Empty);
            var userPassSide = connectionUrl.Split("@")[0];
            var hostSide = connectionUrl.Split("@")[1];

            var user = userPassSide.Split(":")[0];
            var password = userPassSide.Split(":")[1];
            var host = hostSide.Split("/")[0];
            var database = hostSide.Split("/")[1].Split("?")[0];

            return $"Host={host};Database={database};Username={user};Password={password};SSL Made=Require; Trust Server Certificate=true";


        }
    }
}
