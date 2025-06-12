using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using Services;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Data;
using Domain.Contracts;
using Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Persistence.Identity;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public static class InfrastructureServicesRegestiration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services , IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnection"));
            });
            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("IdentityConnection");
                options.UseSqlServer(connectionString);
            });
            Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
            return Services;
        }
    }
}
