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
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Images
{
    public class ImageType : ObjectType<Image>
    {
        protected override void Configure(IObjectTypeDescriptor<Image> descriptor)
        {
            //descriptor
            //    .ImplementsNode()
            //    .IdField(i => i.Id)
            //    .ResolveNode((ctx, id) => ctx.DataLoader<ImageByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            //descriptor.Field(i => i.Labels)
            //    .ResolveWith<ImageResolvers>(r => r.;
        }
    }

    internal class ImageResolvers
    {
        //public async Task<IEnumerable<ImageLabel>> GetLabelsAsync(
        //   Image image,
        //   [ScopedService] TenantFileContext context,
        //   CancellationToken cancellationToken)
        //{
        //    return context.Images.I = ()
        //       .Where(p => p.Id == image.Id)
        //       .Select(r => r.Labels)
        //       ;

        //    //await dataLoader.LoadAsync(cancellationToken, await imageIds);

        //}
    }
}
