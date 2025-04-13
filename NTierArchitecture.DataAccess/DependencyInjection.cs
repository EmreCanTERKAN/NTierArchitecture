using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.DataAccess.Repositories;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.DataAccess;
public static class DependencyInjection 
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("SqlServer");
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
        services.AddIdentityCore<AppUser>(cfg =>
        {
            cfg.Password.RequireNonAlphanumeric = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IAppUserRoleRepository, AppUserRoleRepository>();
        return services;
    }
}
