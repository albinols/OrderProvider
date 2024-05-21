using Microsoft.EntityFrameworkCore;
using OrderProvider.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<OrderNumberTracker> OrderNumberTracker { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderEntity>()
                .HasMany(o => o.OrderItems)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
