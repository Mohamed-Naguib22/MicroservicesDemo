using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Events.Common
{
    public class EventEntity
    {
        public Guid Id { get; set; }
        public string EventType { get; set; }
        public string Data { get; set; }
        public DateTimeOffset OccurredOn { get; set; }
    }
}
