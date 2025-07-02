using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Context
{
    public sealed class EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : DbContext(options)
    {
        public DbSet<EventEntity> Events { get; set; }
    }
}
