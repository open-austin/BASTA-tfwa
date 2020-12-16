using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Addresses
{
    public class AddressType : ObjectType<Address>
    {
        protected override void Configure(IObjectTypeDescriptor<Address> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNodeWith<AddressResolvers>(c => c.GetAddress(default!, default!, default!));

        }
    }
    public class AddressResolvers
    {
        public async Task<Address> GetAddress(Address address,
            //[ScopedService] TenantFileContext context,
            AddressByIdDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync(address.Id, cancellationToken);
        }
    }
}
