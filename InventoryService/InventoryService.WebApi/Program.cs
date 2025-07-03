using InventoryService.Persistence.Extensions;
using InventoryService.Infrastructure.Extensions;
using InventoryService.Application.Extensions;
using InventoryService.WebApi.Extensions;
using InventoryService.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplication();
builder.Services.ConfigureInfrastructure(builder.Configuration);
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
app.UseCors();
app.UseErrorHandler();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
