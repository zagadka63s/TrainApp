using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TrainApp.Data
{
    // Эта фабрика нужна для миграций: EF Core будет создавать через нее твой контекст
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        // Метод, который вызывает EF Core CLI для создания контекста во время миграции
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Чтение настроек из appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Получаем строку подключения из конфигов
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Указываем, что работаем с SQL Server
            optionsBuilder.UseSqlServer(connectionString);

            // Возвращаем новый экземпляр контекста с нужными настройками
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
