using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Data;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;
using TenantFile.Api.Services;
using HotChocolate.Types.Relay;

namespace TenantFile.Api.Models.Properties
{
    [ExtendObjectType(Name = "Query")]
    public class PropertyQueries
    {
        [UseTenantFileContext]
        [UsePaging]
        [HotChocolate.Data.UseFiltering(typeof(PropertyFilterInputType))]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Property> GetProperties([ScopedService] TenantFileContext tenantContext) => tenantContext.Properties.AsQueryable();

        public Task<Property> GetPropertyAsync([ID(nameof(Property))] int id, PropertyByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);

       
    }
}
