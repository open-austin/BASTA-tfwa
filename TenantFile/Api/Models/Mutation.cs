using System;
using System.Collections.Generic;
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
            _context = context;
        }

        public Tenant CreateTenant(CreateTenantInput inputTenant)
        {
            // See if the phone number exists already
            var phone = _context.Phones.FirstOrDefault(x => x.PhoneNumber == inputTenant.PhoneNumber);

            // If it doesn't exist, create a new phone number
            if (phone == null)
            {
                phone = new Phone
                {
                    PhoneNumber = inputTenant.PhoneNumber
                };
            }

            var tenantEntry = _context.Tenants.Add(new Tenant
            {
                Name = inputTenant.Name,
                TenantPhones = new List<TenantPhone>{
                    new TenantPhone {
                        Phone = phone
                    }
                }
            });
            _context.SaveChanges();
            return tenantEntry.Entity;
        }
    }

    public class CreateTenantInput
    {
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
