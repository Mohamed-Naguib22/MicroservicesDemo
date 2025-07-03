using Microsoft.EntityFrameworkCore;
using ProductService.Persistence.Context;

namespace ProductService.WebApi.Extensions
{
    public static class ApplyMigrations
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
            var context = scope.ServiceProvider.GetRequiredService<EventStoreDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
