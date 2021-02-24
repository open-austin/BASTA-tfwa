﻿using HotChocolate;
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
<<<<<<< Updated upstream
                 .Field(p => p.Images)
                .ResolveWith<PhoneResolvers>(r =>  r.GetImagesAsync(default!, default!, default!, default!))
=======
                .Field(p => p.Images)
>>>>>>> Stashed changes
                .UseTenantContext<TenantFileContext>()
                .ResolveWith<PhoneResolvers>(r =>  r.GetImagesAsync(default!, default!, default!, default))
                .Name("images");
<<<<<<< Updated upstream
           
=======
            descriptor
                .Field(p => p.Tenants)
                .UseTenantContext<TenantFileContext>()
                // .UsePaging<NonNullType<TenantType>>()//You can only use this on one of the 
                .ResolveWith<PhoneResolvers>(r =>  r.GetTenantsAsync(default!, default!, default!, default))
                .Name("tenants");
         
>>>>>>> Stashed changes

        }
    }
    class PhoneResolvers
    {
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
    }
}
