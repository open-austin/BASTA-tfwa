using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore;
using System.Reflection;
using Npgsql;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace TenantFile.Api.Models
{
    // https://dev.to/michaelstaib/get-started-with-hot-chocolate-and-entity-framework-e9i
    public class TenantContext : DbContext
    {
        public TenantContext(DbContextOptions<TenantContext> options):base(options){}

        public DbSet<Tenant> Tenants { get; set; } = null!;
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<Phone> Phones { get; set; } = null!;
        public DbSet<Image> Images{ get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Residence> Residences{ get; set; } = null!;
        public DbSet<ResidenceRecord> ResidenceRecords { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}