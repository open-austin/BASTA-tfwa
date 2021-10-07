using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Residences
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class ResidenceQueries
    {
        [UseTenantFileContext]
        [UsePaging]
        [HotChocolate.Data.UseFiltering(typeof(ResidenceFilterInputType))]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Residence> GetResidences([ScopedService] TenantFileContext tenantContext) => tenantContext.Residences.AsQueryable();

        public Task<Residence> GetResidenceByIdAsync([ID(nameof(Residence))] int id, ResidenceByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
        public async Task<IEnumerable<Residence>> GetResidencesByIdAsync([ID(nameof(Residence))] int[] ids, ResidenceByIdDataLoader dataLoader, CancellationToken cancellationToken) => await dataLoader.LoadAsync(ids, cancellationToken);
    }
}