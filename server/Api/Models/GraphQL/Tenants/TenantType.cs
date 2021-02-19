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
                .ResolveWith<TenantResolvers>(resolver => resolver.GetResidenceAsync(default!, default!, default!))
                .Name("residence");
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
            return null;
        }
    }
}