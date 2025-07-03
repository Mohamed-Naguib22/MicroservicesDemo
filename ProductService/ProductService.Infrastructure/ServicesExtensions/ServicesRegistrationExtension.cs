using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductService.Application.Contract.IInfrastructure.IEventDispatcher;
using ProductService.Application.Contract.IInfrastructure.IMessagePublisher;
using ProductService.Infrastructure.Services.EventDispatcher;
using ProductService.Infrastructure.Services.MessagePublisher;
using ProductService.Infrastructure.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.ServicesExtensions
{
    public static class ServicesRegistrationExtension
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostBuilder host)
        {
            services.Configure<RabbitMQSettings>(options =>
            {
                options.UserName = configuration["RabbitMQSettings:UserName"];
                options.Password = configuration["RabbitMQSettings:Password"];
                options.HostName = configuration["RabbitMQSettings:HostName"];
                options.VirtualHost = configuration["RabbitMQSettings:VirtualHost"];
            });

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            host.UseSerilog();

            services.AddScoped<IEventDispatcher, EventDispatcher>();

            services.AddTransient<IMessagePublisher, RabbitMQPublisher>();
        }
    }
}
