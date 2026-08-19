using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Gateway", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCors("Gateway");

app.MapGet("/", () => Results.Ok(new
{
    service = "ApiGateway",
    status = "running",
    routes = new[] { "/productservice/{**catch-all}", "/inventoryservice/{**catch-all}" }
}));

app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapReverseProxy();

app.Run();
