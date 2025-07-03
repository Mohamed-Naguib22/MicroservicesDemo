using ProductService.Application.Extensions;
using ProductService.Infrastructure.ServicesExtensions;
using ProductService.Persistence.Extensions;
using ProductService.WebApi.Endpoints;
using ProductService.WebApi.Extensions;
using ProductService.WebApi.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();
builder.Services.ConfigureInfrastructure(builder.Configuration, builder.Host);

builder.Services.ConfigureSwagger(builder.Configuration);
builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.SwaggerConfig(builder.Configuration, "SwaggerConfigTest");
}
else if (app.Environment.IsProduction())
{
    app.SwaggerConfig(builder.Configuration, "SwaggerConfigProd");
}

app.ApplyMigration();
app.UseHttpsRedirection();
app.UseErrorHandler();
app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapEventsEndpoints();
app.MapProductEndpoints();
app.MapControllers();
app.Run();
