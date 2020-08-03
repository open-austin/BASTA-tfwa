using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore;

namespace TenantFile.Api.Models
{
    // https://dev.to/michaelstaib/get-started-with-hot-chocolate-and-entity-framework-e9i
    public class TenantContext : DbContext
    {
        private string _connectionString;
        public TenantContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("DbUrl");
        }

        public DbSet<Tenant> Tenants { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.ResidenceRecord)
                .WithOne(t => t.Tenant)
                .HasForeignKey(t => t.Id);

            // Create a many to many mapping of Tenants to Phones
            modelBuilder.Entity<TenantPhone>()
                .HasKey(tp => new { tp.TenantId, tp.PhoneId });
            modelBuilder.Entity<TenantPhone>()
                .HasOne(tp => tp.Tenant)
                .WithMany(t => t.TenantPhones)
                .HasForeignKey(t => t.TenantId);
            modelBuilder.Entity<TenantPhone>()
                .HasOne(tp => tp.Phone)
                .WithMany(p => p.TenantPhones)
                .HasForeignKey(tp => tp.PhoneId);
        }
    }
}