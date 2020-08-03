using System;
using System.Linq;
using HotChocolate;
using HotChocolate.Types;

namespace TenantFile.Api.Models
{
    public class Mutation
    {
        private readonly TenantContext _context;
        public Mutation(TenantContext context)
        {
            Console.WriteLine("MUTATING");
            _context = context;
        }
        public Tenant CreateTenant(CreateTenantInput inputTenant)
        {
            var tenantEntry = _context.Tenants.Add(new Tenant
            {
                Name = inputTenant.Name
            });
            Console.WriteLine(inputTenant.Name);
            _context.SaveChanges();
            return tenantEntry.Entity;
        }
    }

    public class CreateTenantInput
    {
        public string Name { get; set; } = null!;
    }

    // public class Greetings
    // {
    //     public string Hello() => "World";
    // }
}
