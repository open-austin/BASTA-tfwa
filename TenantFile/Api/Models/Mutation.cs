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
            Console.WriteLine("FAILURE");
            Console.WriteLine(context.Phones);
            // See if the phone number exists already
            var phone = context.Phones.FirstOrDefault(x => x.PhoneNumber == inputTenant.PhoneNumber);

            
            // If it doesn't exist, create a new phone number
            if (phone == null)
            {
                phone = new Phone
                {
                    PhoneNumber = inputTenant.PhoneNumber
                };
            }
            /* return new Tenant {
                Name = "TEST"
            }; */
            // See if the Mailing Address exists already (there are multiple parts to an address, but we can check just the street number)
            var id = _context.Addresses.FirstOrDefault(x => x.Id == inputTenant.Id).Id;
            var houseNumber = _context.Addresses.FirstOrDefault(x => x.HouseNumber == inputTenant.StreetNumber).HouseNumber;
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





            var tenantEntry = _context.Tenants.Add(new Tenant
            {
                Name = inputTenant.Name,
                TenantPhones = new List<TenantPhone>{
                    new TenantPhone {
                        Phone = phone
                    }
                },
                Addr = new Address{
                    Id = houseNumber,
                    Street = streetName,
                    City = city,
                    PostalCode = zip
                }
            });
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
