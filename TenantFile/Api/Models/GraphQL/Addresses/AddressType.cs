using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .ResolveNodeWith<AddressResolvers>(c => c.GetAddress(default!, default!));

        }
    }
    public class AddressResolvers
    {
        [UseFirstOrDefault]
        public IQueryable<Address> GetAddress(Address address,
            [ScopedService] TenantFileContext context)
        {
            return context.Addresses.AsQueryable().Where(a => a.Id == address.Id);
        }
    }
}
