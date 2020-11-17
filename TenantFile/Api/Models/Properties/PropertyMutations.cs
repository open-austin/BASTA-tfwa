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
                    City = input.City,
                    PostalCode = input.PostalCode,
                    State = input.State,
                    Street = input.Street
                }
            };
            context.Properties.Add(property);
            await context.SaveChangesAsync();

            return new CreatePropertyPayload(property);
        }
    }
}
