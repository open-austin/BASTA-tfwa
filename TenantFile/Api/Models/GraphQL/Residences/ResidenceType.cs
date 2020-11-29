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
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Residences
{
    public class ResidenceType : ObjectType<Residence>
    {

        protected override void Configure(IObjectTypeDescriptor<Residence> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<ResidenceByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
            
            descriptor.Field(r => r.Address)
                      .ResolveWith<ResidenceResolvers>(r => r.GetAddressAsync(default!, default!, default!, default!))
                      .UseDbContext<TenantFileContext>()
                      .Name("address");
        }
    }

    public class ResidenceResolvers
    {
        public Task<Address> GetAddressAsync(
           Residence residence,
           [ScopedService] TenantFileContext context,
           AddressByIdDataLoader dataLoader,
           CancellationToken cancellationToken)
        {
            var addressIds = context.Residences.AsQueryable()//don't use include...add AddresssId to Entity...Address is the Princpal for Property, Residence and Complex
               .Where(p => p.Id == residence.Id)
               .Select(r => r.AddressId)
               .Single();//Could return Address here BUT I believe fetching the ID then passing them all to the Dataloader to make one call to the DB is the benefit ofthe dataloader? n+1?


            return dataLoader.LoadAsync(addressIds, cancellationToken);

        }
    }
}