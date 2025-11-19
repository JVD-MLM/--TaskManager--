using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.IRepositories;
using TaskManager.Application.IServices;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Infrastructure.Services;

namespace TaskManager.Infrastructure;

/// <summary>
///     رجیستر لایه Infrastructure
/// </summary>
public static class InfrastructureServicesRegistration
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtRepository, JwtRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}