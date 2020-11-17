using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using TenantFile.Api.DataLoader;
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
        }
    }
}