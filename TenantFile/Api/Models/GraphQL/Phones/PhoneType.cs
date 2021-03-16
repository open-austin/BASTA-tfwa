using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;
using TenantFile.Api.Models.Tenants;

namespace TenantFile.Api.Models.Phones
{
    public class PhoneType : ObjectType<Phone>
    {

        protected override void Configure(IObjectTypeDescriptor<Phone> descriptor)
        {
            descriptor
              .ImplementsNode()
              .IdField(t => t.Id)
              .ResolveNode((ctx, id) => ctx.DataLoader<PhoneByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(p => p.Images)
                .ResolveWith<PhoneResolvers>(r =>  r.GetImagesAsync(default!, default!, default!, default))
                .UseTenantContext<TenantFileContext>()
                .Name("images");
            descriptor
                .Field(p => p.Tenants)
                .ResolveWith<PhoneResolvers>(r =>  r.GetTenantsAsync(default!, default!, default!, default))
                .UseTenantContext<TenantFileContext>()
                // .UsePaging<NonNullType<TenantType>>()//You can only use this on one of the 
                .Name("tenants");
         

        }
    }
    class PhoneResolvers
    {
        [UseTenantFileContext]
        public async Task<IEnumerable<Image>> GetImagesAsync(
            Phone phone,
            [ScopedService] TenantFileContext context,
            ImageByIdDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            var imagesId = context.Phones.AsQueryable()
                                         .Where(p => p.Id == phone.Id)
                                         .SelectMany(p => p.Images.Select(i => i.Id))
                                         .ToArrayAsync();

            return await dataLoader.LoadAsync(cancellationToken, await imagesId);
            
        }
        [UseTenantFileContext]
        public async Task<IEnumerable<Tenant>> GetTenantsAsync(
            Phone phone,
            [ScopedService] TenantFileContext context,
            TenantByIdDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            int[] phoneIds = await context.Phones.AsQueryable()
                                         .Where(p => p.Id == phone.Id)
                                         //.Include(e => e.Tenants)
                                         .SelectMany(p => p.Tenants.Select(i => i.Id))
                                         .ToArrayAsync();

            return await dataLoader.LoadAsync(phoneIds, cancellationToken);
            
        }
    }
}
