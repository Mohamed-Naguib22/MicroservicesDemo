using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Domain.Events
{
    public sealed record ProductCreatedEvent
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
