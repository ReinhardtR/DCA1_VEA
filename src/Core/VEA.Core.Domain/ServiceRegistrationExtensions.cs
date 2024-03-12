using Microsoft.Extensions.DependencyInjection;
using VEA.Core.Domain.Common;

namespace VEA.Core.Domain;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentTime, CurrentTime>();

        return services;
    }
}