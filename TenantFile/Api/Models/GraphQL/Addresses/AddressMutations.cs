using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using TenantFile.Api.Models.Tenants;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;
using TenantFile.Api.Services;
using TenantFile.Api.Models.Addresses;

namespace TenantFile.Api.Models.Properties
{
    [ExtendObjectType(Name = "Mutation")]
    public class AddressMutations
    {
        public async Task<VerifyAddressPayload> VerifyAddress([Service] IAddressVerificationService service, VerifyAddressInput address)
        {
            return new VerifyAddressPayload(await service.VerifyAddressAsync(address));
        }
    }
}