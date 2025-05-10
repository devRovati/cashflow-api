using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using CashFlowApi.Infrastructure.Persistence;

namespace CashFlowApi.WebApi;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31)));

        return new ApplicationDbContext(builder.Options);
    }
}
