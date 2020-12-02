using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Images
{
    [ExtendObjectType(Name = "Query")]
    public class ImageQueries
    {
        [UseTenantFileContext]
        [UsePaging]
        //[UseSelection]
        [HotChocolate.Data.UseFiltering(typeof(ImageFilterInputType))]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Image> GetImages([ScopedService] TenantFileContext tenantContext) => tenantContext.Images.AsNoTracking();

        public Task<Image> GetImageAsync(int id, DataLoaderById<Image> dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
    }
}
