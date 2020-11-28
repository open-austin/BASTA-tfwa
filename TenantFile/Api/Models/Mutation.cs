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

        public Tenant CreateTenant([Service] TenantContext context, CreateTenantInput inputTenant)
        {
            // See if the Tenant is already in the system
            var tenant_id = context.Tenants.FirstOrDefault(x => x.Id == inputTenant.Id).Id;
            var tenant_name = context.Tenants.FirstOrDefault(x => x.Name == inputTenant.Name).Name;
            var phone = context.Phones.FirstOrDefault(x => x.PhoneNumber == inputTenant.PhoneNumber);

            var tenant = new Tenant {};

            // To Do: If any of the fields are missing, we'll keep what's already in the system (if anything) and enter the rest
            // Right now, it just adds regardless of whether tenant is in system or not
            /* if (tenant_id <= 0) {
                int i = 0;
                while (true) {
                    
                    if (context.Tenants.Id == 0) {
                        break;
                    }
                }
            }
            if (phone == null) {
                phone = new Phone
                {
                    PhoneNumber = inputTenant.PhoneNumber
                };
            } */
            if (tenant_name == null) {
                
                tenant = new Tenant
                {
                    Id = inputTenant.Id, // Need to fix this
                    Name = inputTenant.Name,
                    ResidenceRecords = new List<ResidenceRecord>(),
                    TenantPhones = new List<TenantPhone>()
                };

                tenant.ResidenceRecords.Add(new ResidenceRecord{
                    Residence = new Residence{
                        Property = new Property{
                            Address = new Address{
                                StreetNumber = Convert.ToString(inputTenant.StreetNumber),
                                Street = inputTenant.StreetName,
                                City = inputTenant.City,
                                State = "TX",
                                PostalCode = inputTenant.ZipCode
                            }
                        }
                    }
                });

                tenant.TenantPhones.Add(new TenantPhone{
                    Phone = new Phone{
                        PhoneNumber = inputTenant.PhoneNumber
                    }
                });
            }
            
            
/*             // See if the Mailing Address exists already (there are multiple parts to an address, but we can check just the street number)
            var id = _context.Addresses.FirstOrDefault(x => x.Id == inputTenant.Id).Id;
            var houseNumber = Convert.ToInt32(_context.Addresses.FirstOrDefault(x => Convert.ToInt32(x.StreetNumber) == inputTenant.StreetNumber).StreetNumber);
            var streetName = _context.Addresses.FirstOrDefault(x => x.Street == inputTenant.StreetName).Street;
            var city = _context.Addresses.FirstOrDefault(x => x.City == inputTenant.City).City;
            var zip = _context.Addresses.FirstOrDefault(x => x.PostalCode == inputTenant.ZipCode).PostalCode;

            // If it isn't set, create a new Mailing Address
            if (houseNumber <= 0)
            {
                id = inputTenant.Id;
                houseNumber = inputTenant.StreetNumber;
                streetName = inputTenant.StreetName;
                city = inputTenant.City;
                zip = inputTenant.ZipCode;
            }

 */



            var tenantEntry = _context.Tenants.Add(tenant);
            _context.SaveChanges();
            return tenantEntry.Entity;
        }
    }

    public class CreateTenantInput
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        // beginning Mailing Address fields:
        public int StreetNumber { get; set; } = -1;
        public string StreetName { get; set; } = null!;
        public string City { get; set; } = null!;
        public int ZipCode {get; set; } = -1;
        // end

    }
}
