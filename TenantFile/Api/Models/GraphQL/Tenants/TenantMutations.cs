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
      // See if the phone number exists already
      Phone? phone = context.Phones.FirstOrDefault(x => x.PhoneNumber == inputTenant.PhoneNumber);

      // If it doesn't exist, create a new phone number
      if (phone == null)
      {
        phone = new Phone
        {
          PhoneNumber = inputTenant.PhoneNumber
        };
      }
      var tenant = new Tenant
      {
        Name = inputTenant.Name,
        Phones = new List<Phone> {
                    phone
                },
      };

      var tenantEntry = context.Tenants.Add(tenant);
      await context.SaveChangesAsync(cancellationToken);
      return new CreateTenantPayload(tenant);
    }

  }
}

