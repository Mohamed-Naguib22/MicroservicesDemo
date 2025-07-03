using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Events.ProductEvents
{
    public sealed record ProductCreatedEvent
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
