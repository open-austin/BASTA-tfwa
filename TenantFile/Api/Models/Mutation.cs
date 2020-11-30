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



            var tenant = new Tenant {}; // This is a hacky workaround.  Ask Ryan how to make the "Tenant" attribute of the "ResidenceRecord" refer to the actual Tenant you're adding.
            var residence_records = new List<ResidenceRecord> {};
            var residences = new List<Residence> {};


            


            var propertyEntry = _context.Properties.Add(new Property
            {
                UnitIdentifier = "3B",
                    Address = new Address {
                        StreetNumber = Convert.ToString(inputTenant.HouseNumber),
                        Street = inputTenant.Street,
                        City = inputTenant.City,
                        State = "TX",
                        PostalCode = inputTenant.ZipCode
                    },
                    Residences = residences
            });
            _context.SaveChanges();


            var tenantEntry = _context.Tenants.Add(new Tenant
            {
                Name = inputTenant.Name,
                ResidenceRecords = new List<ResidenceRecord>{
                    new ResidenceRecord {
                        MoveIn = new DateTime(2020, 11, 28),
                        Tenant = tenant,
                        Residence = new Residence {
                            UnitIdentifier = "3B",
                            ResidenceRecords = residence_records,
                            Property = propertyEntry.Entity
                        }
                    }
                },
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
        public int HouseNumber { get; set; } = -1;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public int ZipCode { get; set; } = -1;
        public string PropertyName { get; set; } = null!;
    }
}
