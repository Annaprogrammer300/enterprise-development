using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Polyclinic.Infrastructure.EfCore;

public class PolyclinicDbContextFactory : IDesignTimeDbContextFactory<PolyclinicDbContext>
{
    public PolyclinicDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false) 
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");


        var optionsBuilder = new DbContextOptionsBuilder<PolyclinicDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new PolyclinicDbContext(optionsBuilder.Options);
    }
}