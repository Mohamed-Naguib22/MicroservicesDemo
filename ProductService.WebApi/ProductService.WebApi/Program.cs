using ProductService.Application.Extensions;
using ProductService.Infrastructure.ServicesExtensions;
using ProductService.Persistence.Extensions;
using ProductService.WebApi.Endpoints;
using ProductService.WebApi.Extensions;
using ProductService.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();
builder.Services.ConfigureInfrastructure();

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
app.UseCors();
app.UseErrorHandler();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapProductEndpoints();
app.MapControllers();
app.Run();
