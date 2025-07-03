using InventoryService.Application.Behaviors;
using InventoryService.Infrastructure.Services.MessageConsumer.ProductConsumers;
using InventoryService.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Infrastructure.Extensions
{
    public static class ServicesRegistrationExtension
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostBuilder host)
        {
            services.AddHostedService<ProductDeletedConsumer>();
            services.AddHostedService<ProductCreatedConsumer>();
            services.AddHostedService<ProductUpdatedConsumer>();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            host.UseSerilog();

            services.Configure<RabbitMQSettings>(options =>
            {
                options.UserName = configuration["RabbitMQSettings:UserName"];
                options.Password = configuration["RabbitMQSettings:Password"];
                options.HostName = configuration["RabbitMQSettings:HostName"];
                options.VirtualHost = configuration["RabbitMQSettings:VirtualHost"];
            });
        }
    }
}
