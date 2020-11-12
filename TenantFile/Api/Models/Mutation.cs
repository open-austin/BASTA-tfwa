using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using Microsoft.AspNetCore.Mvc;
using Twilio.Http;
using TenantFile.Api.Models.Tenants;

namespace TenantFile.Api.Models
{
    public class Mutation
    {
        private readonly TenantFileContext _context;
        public Mutation([Service] TenantFileContext context)
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
                Phones = new List<Phone>{
                    new Phone {
                        PhoneNumber = phone.PhoneNumber
                    }
                }
            });
            _context.SaveChanges();
            return tenantEntry.Entity;
        }

    }

}
