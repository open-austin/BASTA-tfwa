using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore;
using System.Reflection;
using Npgsql;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using TenantFile.Api.Models.Entities;
using TenantFile.Api.Configurations;

namespace TenantFile.Api.Models
{
    public class TenantFileContext : DbContext
    {
        public TenantFileContext(DbContextOptions<TenantFileContext> options):base(options){}

        public DbSet<Tenant> Tenants { get; set; } = default!;
        public DbSet<Organizer> Organizers { get; set; } = null!;
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<Phone> Phones { get; set; } = null!;
        public DbSet<Image> Images{ get; set; } = null!;
        //TODO: Determine if these should be owned type as well
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Residence> Residences{ get; set; } = null!;
        public DbSet<ResidenceRecord> ResidenceRecords { get; set; } = null!;
        //public DbSet<TenantEvent> TenantEvents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}