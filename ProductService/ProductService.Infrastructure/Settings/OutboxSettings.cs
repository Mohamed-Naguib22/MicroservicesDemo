using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Settings
{
    public sealed class OutboxSettings
    {
        public int PollingIntervalSeconds { get; set; } = 5;
        public int BatchSize { get; set; } = 20;
        public int MaxRetryCount { get; set; } = 10;
    }
}