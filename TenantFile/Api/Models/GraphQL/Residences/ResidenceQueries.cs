using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Residences
{
    [ExtendObjectType(Name = "Query")]
    public class ResidenceQueries
    {
        [UseTenantFileContext]
        [UsePaging]
        //[UseSelection]
        [HotChocolate.Data.UseFiltering(typeof(ResidenceFilterInputType))]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Residence> GetResidences([ScopedService] TenantFileContext tenantContext) => tenantContext.Residences.AsQueryable();

        public Task<Residence> GetResidenceAsync(int id, DataLoaderById<Residence> dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
    }
}