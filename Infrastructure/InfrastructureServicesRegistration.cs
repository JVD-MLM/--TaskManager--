using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.IRepositories;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure;

/// <summary>
///     رجیستر لایه Infrastructure
/// </summary>
public static class InfrastructureServicesRegistration
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtRepository, JwtRepository>();
    }
}