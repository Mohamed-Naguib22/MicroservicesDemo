using InventoryService.Application.Contract.IInfrastructure.ICaching;
using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Domain.Constants;
using InventoryService.Persistence.Caching;
using InventoryService.Persistence.Repositories;
using InventoryService.Persistence.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Persistence.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var mongoConnectionString = configuration.GetSection("MongoSettings:ConnectionString").Value;
            var mongoDatabaseName = configuration.GetSection("MongoSettings:DatabaseName").Value;

            var mongoClient = new MongoClient(mongoConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);

            services.AddSingleton(mongoDatabase);

            var redisConnectionString = configuration.GetSection("ConnectionStrings:Redis").Value;

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
                return connectionMultiplexer;
            });

            services.AddScoped<ICachingService, RedisCacheService>();
        }
    }
}
