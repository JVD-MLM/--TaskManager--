using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Validators.Authentication;

namespace TaskManager.Application;

/// <summary>
///     رجیستر لایه Application
/// </summary>
public static class ApplicationServicesRegistration
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        // automatic import all validators
        services.AddValidatorsFromAssemblyContaining<SignUpRequestValidator>();
    }
}