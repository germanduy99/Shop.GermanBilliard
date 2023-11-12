using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shop.GermanBilliard.Domain;
using Shop.GermanBilliard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Infrastructure
{
    public class BilliardDbContext : DbContext
    {
        public BilliardDbContext(DbContextOptions<BilliardDbContext> options) : base(options)
        {
        }

        public DbSet<Cue> Cues { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                entry.Entity.CreatedBy = "Admin";
                entry.Entity.LastModifiedBy = "Admin";

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
       

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BilliardDbContext).Assembly);
            

            modelBuilder.Entity<Cue>()
                  .HasOne(cue => cue.Brand)
                  .WithMany()
                  .HasForeignKey(cue => cue.BrandId)
                  .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<OrderItem>()
                  .HasOne(orderItem => orderItem.Cue)
                  .WithMany()
                  .HasForeignKey(orderItem => orderItem.CueId)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                  .HasOne(orderItem => orderItem.Order)
                  .WithMany()
                  .HasForeignKey(orderItem => orderItem.OrderId)
                  .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
