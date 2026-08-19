using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Entities.Common
{
    public class EventEntity
    {
        public Guid Id { get; set; }
        public string EventType { get; set; }
        public string Data { get; set; }
        public DateTimeOffset OccurredOn { get; set; }
        public bool IsPublished { get; set; }
        public DateTimeOffset? PublishedOn { get; set; }
        public int RetryCount { get; set; }
        public string? LastError { get; set; }
    }
}
