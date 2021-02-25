using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Tenants
{
  [ExtendObjectType(Name = "Mutation")]
  public class TenantMutations
  {
    [UseTenantFileContext]
    public async Task<CreateTenantPayload> CreateTenantAsync(
        CreateTenantInput inputTenant,
        [ScopedService] TenantFileContext context,
        CancellationToken cancellationToken)
    {
        var (nameInput, phoneInput, residenceInput, residenceIdInput) = inputTenant;
      // See if the phone number exists already
      Phone? phone = context.Phones.FirstOrDefault(x => x.PhoneNumber == inputTenant.PhoneNumber);

      // If it doesn't exist, create a new phone number
            if (phone == null)
            {
                phone = new Phone
                {
                    PhoneNumber = phoneInput
                };
            }
            var tenant = new Tenant
            {
                Name = nameInput,
                Phones = new List<Phone> {
                    phone
                },
         };

            if (residenceInput != null)
            {
                var (line1, line2, line3, line4, city, state, postalCode) = residenceInput!.AddressInput;
                tenant.CurrentResidence = new Residence()
                {
                    Address = new Address()
                    {
                        Line1 = line1,
                        Line2 = line2,
                        Line3 = line3,
                        Line4 = line4,
                        City = city,
                        State = state,
                        PostalCode = postalCode

                    }
                };
            }
            else
            {
                tenant.ResidenceId = residenceIdInput!;
            }

      var tenantEntry = context.Tenants.Add(tenant);
      await context.SaveChangesAsync(cancellationToken);
      return new CreateTenantPayload(tenant);
    }

  }
}

