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


        }
    }
    class PropertyResolvers
    {
        public async Task<IEnumerable<Residence>> GetResidencesAsync(
            Property property,
            [ScopedService] TenantFileContext context,
            ResidenceByPropertyDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            var residenceIds = await context.Properties.AsAsyncEnumerable()
               .Where(p => p.Id == property.Id)
               .Select(r=>r.Id)
               .ToArrayAsync();

            return await dataLoader.LoadAsync(cancellationToken, residenceIds);

        }
    }
}