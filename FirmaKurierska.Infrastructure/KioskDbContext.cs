using Microsoft.EntityFrameworkCore;
using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Infrastructure
{
    public class KioskDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public KioskDbContext(DbContextOptions<KioskDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Courier)
                .WithMany(c => c.Addresses)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Addresses)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(a => a.Courier)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
