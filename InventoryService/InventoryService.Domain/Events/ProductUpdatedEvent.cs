using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Domain.Events
{
    public sealed record ProductUpdatedEvent
    {
        public string ProductId { get; init; }
        public UpdatedProduct UpdatedProduct { get; init; }
    }

    public sealed record UpdatedProduct(string? Name, int? Quantity);
}
