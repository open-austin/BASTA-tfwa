using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                .ResolveNode((ctx, id) => ctx.DataLoader<AddressByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            //        descriptor
            //                .Field(p => p.Residences)
            //                .ResolveWith<PropertyResolvers>(r => r.GetResidencesAsync(default!, default!, default!, default!))
            //                .UseDbContext<TenantFileContext>()
            //                .Name("residences");


            //    }
            //}
            //class PropertyResolvers
            //{
            //    public async Task<IEnumerable<Residence>> GetResidencesAsync(
            //        Property property,
            //        [ScopedService] TenantFileContext context,
            //        ResidenceByPropertyDataLoader dataLoader,
            //        CancellationToken cancellationToken)
            //    {
            //        var residenceIds = await context.Properties.AsAsyncEnumerable()
            //           .Where(p => p.Id == property.Id)
            //           .Select(r => r.Id)
            //           .ToArrayAsync();

            //        return await dataLoader.LoadAsync(cancellationToken, residenceIds);

            //    }
            //}
        }
    }
}
