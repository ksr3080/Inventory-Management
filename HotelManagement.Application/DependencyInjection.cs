using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HotelManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(Common.Behaviors.ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddAutoMapper(configration => { }, assembly);

        return services;
    }
}