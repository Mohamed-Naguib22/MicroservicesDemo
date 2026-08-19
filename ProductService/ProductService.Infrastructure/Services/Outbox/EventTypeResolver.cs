using ProductService.Domain.Events.ProductEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Services.Outbox
{
    public static class EventTypeResolver
    {
        private static readonly IReadOnlyDictionary<string, Type> EventTypesByName =
            typeof(IDomainEvent).Assembly
                .GetTypes()
                .Where(t => t.IsClass && typeof(IDomainEvent).IsAssignableFrom(t))
                .ToDictionary(t => t.Name, t => t);

        public static bool TryResolve(string eventType, out Type? type) =>
            EventTypesByName.TryGetValue(eventType, out type);
    }
}