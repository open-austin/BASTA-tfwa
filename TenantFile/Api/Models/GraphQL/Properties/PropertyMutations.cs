using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;
using TenantFile.Api.Services;

namespace TenantFile.Api.Models.Properties
{
    [ExtendObjectType(Name = "Mutation")]
    public class PropertyMutations
    {
        [UseTenantFileContext]
        public async Task<CreatePropertyPayload> CreateProperty(CreatePropertyInput input,
            [ScopedService] TenantFileContext context)
       
        {
            var property = new Property
            {
                Name = input.Name
            ,
                Address = new Address()
                {
                    City = input.AddressInput.City,
                    PostalCode = input.AddressInput.PostalCode,
                    State = input.AddressInput.State,
                    Line1 = input.AddressInput.Line1,
                    Line2 = input.AddressInput.Line2,
                    Line3 = input.AddressInput.Line3,
                    Line4 = input.AddressInput.Line4
                }
            };
            context.Properties.Add(property);
            await context.SaveChangesAsync();

            return new CreatePropertyPayload(property);
        }

        public Task<Address?> VerifyAddress([Service]IAddressVerificationService service, Address address)
        {
            return service.VerifyAddressAsync(address);
        }
    }
}
