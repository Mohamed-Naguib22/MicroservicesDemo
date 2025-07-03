using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities.Common;
using ProductService.Persistence.Extensions.EntityConfigurations;
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
            modelBuilder.ApplyConfiguration(new EventConfiguration());
        }

        public DbSet<EventEntity> Events { get; set; }
    }
}
