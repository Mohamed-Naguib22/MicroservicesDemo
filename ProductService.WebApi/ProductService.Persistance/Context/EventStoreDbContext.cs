using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Persistence.Context
{
    public sealed class EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<EventEntity> Events { get; set; }
    }
}
