using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskManager.Infrastructure;

/// <summary>
///     فکتوری کانتکست دیتابیس
/// </summary>
public class TaskManagerDbContextFactory : IDesignTimeDbContextFactory<TaskManagerDbContext>
{
    public TaskManagerDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TaskManagerDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));

        return new TaskManagerDbContext(optionsBuilder.Options);
    }
}