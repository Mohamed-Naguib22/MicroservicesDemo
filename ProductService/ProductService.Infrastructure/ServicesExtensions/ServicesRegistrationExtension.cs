using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Contract.IInfrastructure.IEventDispatcher;
using ProductService.Application.Contract.IInfrastructure.IMessagePublisher;
using ProductService.Infrastructure.Services.EventDispatcher;
using ProductService.Infrastructure.Services.MessagePublisher;
using ProductService.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.ServicesExtensions
{
    public static class ServicesRegistrationExtension
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEventDispatcher, EventDispatcher>();

            services.AddTransient<IMessagePublisher, RabbitMQPublisher>();
        }
    }
}
