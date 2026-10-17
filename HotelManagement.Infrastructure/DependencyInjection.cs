using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HotelManagement.Infrastructure.Repositories.Interfaces;
using HotelManagement.Infrastructure.Repositories.Repository;

namespace HotelManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        string connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseSqlServer(connectionString);
            });
        services.AddHttpContextAccessor();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IFormRepository, FormRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IFormBusinessRulesRepository, FormBusinessRulesRepository>();
        services.AddScoped<IFormRulesPermissionRepository, FormRulesPermissionRepository>();
        services.AddScoped<IUsersInRoleRepository, UsersInRoleRepository>();
        services.AddScoped<Authentication.IJwtTokenGenerator, Authentication.JwtTokenGenerator>();
        services.AddScoped<Authentication.IPasswordHasher, Authentication.PasswordHasher>();
        return services;
    }
}