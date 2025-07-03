using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using ProductService.Persistence.Context;
using ProductService.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Persistence.Extensions
{
    public static class ServicesRegistrationExtension
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EventStoreDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("EventStore"))
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
