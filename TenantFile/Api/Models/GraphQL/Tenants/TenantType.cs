using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Tenants
{
    public class TenantType : ObjectType<Tenant>
    {
      
        protected override void Configure(IObjectTypeDescriptor<Tenant> descriptor)
        {
            //descriptor
            //    .ImplementsNode()
            //    .IdField(t => t.Id)
            //    .ResolveNode((ctx, id) => ctx.DataLoader<DataLoaderById<Tenant>>().LoadAsync(id, ctx.RequestAborted));
        }
    }
}