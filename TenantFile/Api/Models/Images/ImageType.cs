using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Images
{
    public class ImageType : ObjectType<Image>
    {
        protected override void Configure(IObjectTypeDescriptor<Image> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(i => i.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<ImageByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
        }
    }
}
