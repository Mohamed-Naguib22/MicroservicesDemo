using InventoryService.Application.Contract.IInfrastructure.ICaching;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application.Behaviors.Caching
{
    public sealed class CacheInvalidationBehavior<TRequest, TResponse>(ICachingService cache)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request is not ICacheInvalidatingRequest invalidatingRequest)
                return await next(cancellationToken);

            var response = await next(cancellationToken);

            await cache.DeleteByPatternAsync(invalidatingRequest.CacheKeyToInvalidate);
            return response;
        }
    }
}
