using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ProductService.WebApi.Extensions
{
    public static class SwaggerExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var testToken = configuration.GetSection("AppSettings:TestToken").Value;

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Service API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = testToken,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });
            });
        }

        public static void SwaggerConfig(this IApplicationBuilder app, IConfiguration configuration, string SwaggerConfigName)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string endPoint = configuration[$"{SwaggerConfigName}:EndPoint"];
                string title = configuration[$"{SwaggerConfigName}:Title"];
                c.SwaggerEndpoint(endPoint, title);
                c.DocumentTitle = $"{title} Documentation";
                c.DocExpansion(DocExpansion.List);
            });
        }
    }
}
