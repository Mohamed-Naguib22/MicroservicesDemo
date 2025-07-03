using InventoryService.Persistence.Extensions;
using InventoryService.Infrastructure.Extensions;
using InventoryService.Application.Extensions;
using InventoryService.WebApi.Extensions;
using InventoryService.WebApi.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:5100");

builder.Services.ConfigureApplication();
builder.Services.ConfigureInfrastructure(builder.Configuration, builder.Host);
builder.Services.ConfigurePersistence(builder.Configuration);

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

app.UseHttpsRedirection();
app.UseErrorHandler();
app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
