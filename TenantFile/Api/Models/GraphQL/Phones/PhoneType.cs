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

namespace TenantFile.Api.Models.Phones
{
    public class PhoneType : ObjectType<Phone>
    {

        protected override void Configure(IObjectTypeDescriptor<Phone> descriptor)
        {
            //descriptor
            //   .ImplementsNode()
            //   .IdField(t => t.Id)
            //   .ResolveNode((ctx, id) => ctx.DataLoader<DataLoaderById<Phone>>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                 .Field(p => p.Images)
                .ResolveWith<PhoneResolvers>(r =>  r.GetImagesAsync(default!, default!, default!, default!))
                .UseDbContext<TenantFileContext>()
                .Name("images");
           

        }
    }
    class PhoneResolvers
    {
        public async Task<IEnumerable<Image>> GetImagesAsync(
            Phone phone,
            [ScopedService] TenantFileContext context,
            DataLoaderById<Image> dataLoader,
            CancellationToken cancellationToken)
        {
            var imagesId = context.Phones.AsQueryable()
                                         .Where(p => p.Id == phone.Id)
                                         .SelectMany(p => p.Images.Select(i => i.Id))
                                         .ToArrayAsync();

            return await dataLoader.LoadAsync(cancellationToken, await imagesId);
            
        }
    }
}
