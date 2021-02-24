using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Models.Entities;

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
<<<<<<< Updated upstream
                .ResolveWith<TenantResolvers>(resolver => resolver.GetResidenceAsync(default!, default!, default!))
                .Name("residence");
=======
                .UseTenantContext<TenantFileContext>()
                .ResolveWith<TenantResolvers>(resolver => resolver.GetResidenceAsync(default!, default!, default))
                .Name("residence");
            descriptor
                .Field(t => t.Phones)
                .UseTenantContext<TenantFileContext>()
                .UsePaging<NonNullType<PhoneType>>()
                .ResolveWith<TenantResolvers>(resolver => resolver.GetPhonesAsync(default!, default!, default!, default))
                .Name("phones");
            descriptor
                .Field(t => t.ResidenceId)
                .ID(nameof(Residence));
>>>>>>> Stashed changes
        }
    }
    public class TenantResolvers
    {
        public Task<Residence>? GetResidenceAsync(
          Tenant tenant,
          ResidenceByIdDataLoader dataLoader,
          CancellationToken cancellationToken)
        {
            if (tenant.ResidenceId != null)
            {
                return dataLoader.LoadAsync((int)tenant.ResidenceId, cancellationToken);
            }
<<<<<<< Updated upstream
            return null;
=======
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
                             //.Include(t => t.Phones)
                                      .Where(t => t.Id == tenant.Id)
                                      .SelectMany(t => t.Phones.Select(p => p.Id))
                                      .ToArrayAsync();

         
            return await dataLoader.LoadAsync(phoneIds, cancellationToken );
>>>>>>> Stashed changes
        }
    }
}