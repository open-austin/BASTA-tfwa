using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;
using TenantFile.Api.Models.Phones;

namespace TenantFile.Api.Models.Tenants
{
    public class TenantType : ObjectType<Tenant>
    {

        protected override void Configure(IObjectTypeDescriptor<Tenant> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<TenantByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
            descriptor
                .Field(t => t.CurrentResidence)
                .ResolveWith<TenantResolvers>(resolver => resolver.GetResidenceAsync(default!, default!, default))
                .UseTenantContext<TenantFileContext>()
                .Name("residence");
            descriptor
                .Field(t => t.Phones)
                .ResolveWith<TenantResolvers>(resolver => resolver.GetPhonesAsync(default!, default!, default!, default))
                .UseTenantContext<TenantFileContext>()
                .UsePaging<NonNullType<PhoneType>>()
                .Name("phones");
            descriptor
                .Field(t => t.ResidenceId)
                .ID(nameof(Residence));
        }
    }
    public class TenantResolvers
    {
        public async Task<Residence?> GetResidenceAsync(
          Tenant tenant,
          ResidenceByIdDataLoader dataLoader,
          CancellationToken cancellationToken)
        {
            if (tenant.ResidenceId is null)
            {
                return null;
            }
            return await dataLoader.LoadAsync((int)tenant.ResidenceId, cancellationToken);
        }
        [UseTenantFileContext]
        public async Task<IEnumerable<Phone>> GetPhonesAsync(
         Tenant tenant,
         [ScopedService] TenantFileContext dbContext,
         PhoneByIdDataLoader dataLoader,
         CancellationToken cancellationToken
     )
        {

            int[] phoneIds = await dbContext.Tenants
                             .AsQueryable()
                             .Include(t => t.Phones)
                                      .Where(t => t.Id == tenant.Id)
                                      .SelectMany(t => t.Phones.Select(p => p.Id))
                                      .ToArrayAsync();

            // var phoneIds = tenant.Phones.Select(p => p.Id ).ToArray();
            // phoneIds.ToList().ForEach(p => Debug.WriteLine($"Phone Id: {p}")); 
            // phoneIds.ToList().ForEach(p => Console.WriteLine($"Phone Id: {p}")); 
            return await dataLoader.LoadAsync(phoneIds, cancellationToken );
        }
    }
}