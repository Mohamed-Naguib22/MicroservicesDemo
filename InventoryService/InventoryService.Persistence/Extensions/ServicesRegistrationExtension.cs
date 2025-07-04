﻿using InventoryService.Application.Contract.IInfrastructure.ICaching;
using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Domain.Constants;
using InventoryService.Persistence.Caching;
using InventoryService.Persistence.Repositories;
using InventoryService.Persistence.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
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

            var section = configuration.GetSection("MongoSettings");

            var mongoConfig = section.Get<MongoSettings>();
            var mongoClient = new MongoClient(mongoConfig.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoConfig.DatabaseName);

            services.AddSingleton(mongoDatabase);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = RedisKeys.PRODUCTS_KEY;
            });

            services.AddScoped<ICachingService, RedisCacheService>();
        }
    }
}
