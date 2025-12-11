using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Polyclinic.Infrastructure.EfCore;

public class PolyclinicDbContextFactory : IDesignTimeDbContextFactory<PolyclinicDbContext>
{
    public PolyclinicDbContext CreateDbContext(string[] args)
    {
        // 1. Указываем путь к ТЕКУЩЕМУ проекту (где лежит фабрика и appsettings.Development.json)
        var basePath = Directory.GetCurrentDirectory();

        // 2. Настраиваем загрузку конфигурации из JSON-файла в текущей папке
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false) // Основной файл
            .Build();

        // 3. Получаем строку подключения из конфигурации
        var connectionString = configuration.GetConnectionString("DefaultConnection");


        // 4. Создаем DbContext с провайдером для PostgreSQL
        var optionsBuilder = new DbContextOptionsBuilder<PolyclinicDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new PolyclinicDbContext(optionsBuilder.Options);
    }
}