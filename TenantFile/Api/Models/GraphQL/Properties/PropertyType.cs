using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Addresses;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Properties
{
    public class PropertyType : ObjectType<Property>
    {

        protected override void Configure(IObjectTypeDescriptor<Property> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<PropertyByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                    .Field(p => p.Residences)
                    .ResolveWith<PropertyResolvers>(r => r.GetResidencesAsync(default!, default!, default!, default!))
                    .UseDbContext<TenantFileContext>()
                    .Name("residences");
            descriptor
                    .Field(p => p.Address)
                    .ResolveWith<PropertyResolvers>(r => r.GetAddressAsync(default!, default!, default!, default!))
                    .UseDbContext<TenantFileContext>()
                    .Name("address");


        }
    }
    class PropertyResolvers
    {
        public async Task<IEnumerable<Residence>> GetResidencesAsync(
            Property property,
            [ScopedService] TenantFileContext context,
            ResidenceByIdDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            var residenceIds = context.Properties.AsQueryable()
               .Where(p => p.Id == property.Id)
               .Select(r => r.Id)
               .ToArrayAsync();

            return await dataLoader.LoadAsync(cancellationToken, await residenceIds);

        }
        public Task<Address> GetAddressAsync(
            Property property,
            [ScopedService] TenantFileContext context,
            AddressByIdDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            var addressIds = context.Properties.AsQueryable()//don't use include...add AddresssId to Entity...Address is the Princpal for Property, Residence and Complex
               .Where(p => p.Id == property.Id)
               .Select(r => r.AddressId)
               .Single();//Could return Address here BUT I believe fetching the ID then passing them all to the Dataloader to make one call to the DB is the benefit ofthe dataloader? n+1?
               

            return dataLoader.LoadAsync(addressIds, cancellationToken);

        }
        //public async Task<IEnumerable<Address>> GetAddressAsync(
        //    Property property,
        //    [ScopedService] TenantFileContext context,
        //    AddressByIdDataLoader dataLoader,
        //    CancellationToken cancellationToken)
        //{
        //    int[] addressIds = await context.Properties.Include(p=>p.Address).AsAsyncEnumerable()//don't use include...add AddresssId ot Entity
        //       .Where(p => p.Id == property.Id)
        //       .Select(r=>r.Address.Id)//Could return Address here BUT I believe fetching the ID then passing them all to the Dataloader to make one call to the DB is the benefit ofthe dataloader? n+1?
        //       .ToArrayAsync();

        //    return await dataLoader.LoadAsync(cancellationToken, addressIds);

    }
}
