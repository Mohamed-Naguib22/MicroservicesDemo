using Microsoft.AspNetCore.Diagnostics;
using ProductService.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProductService.WebApi.Middlewares
{
    public static class ErrorHandlerMiddleware
    {
        public static void UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null) return;

                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.ContentType = "application/json";

                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        BadRequestException => (int)HttpStatusCode.BadRequest,
                        OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                        HttpRequestException => (int)HttpStatusCode.BadGateway,
                        _ => (int)HttpStatusCode.InternalServerError
                    };

                    var errorResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = contextFeature.Error.GetBaseException().Message,
                        Errors = contextFeature.Error switch
                        {
                            BadRequestException badRequestError => badRequestError.Errors,
                            _ => null
                        }
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                });
            });
        }
    }
}
