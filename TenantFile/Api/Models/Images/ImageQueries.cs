using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
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
        [UseFiltering(FilterType = typeof(ImageFilterInputType))]
        [UseSorting]
        public IQueryable<Image> GetImages([ScopedService] TenantFileContext tenantContext) => tenantContext.Images.AsQueryable();

        public Task<Image> GetImageAsync(int id, ImageByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
    }
}
