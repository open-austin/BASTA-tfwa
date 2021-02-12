using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models;
using TenantFile.Api.Models.Entities;
using HotChocolate.Types.Relay;
using TenantFile.Api.Models.Tenants;
using System.Collections.Generic;

namespace TenantFile.Api.Tenants
{
    [ExtendObjectType(Name = "Query")]
    public class TenantQueries
    {
        [UseTenantFileContext]
        [UsePaging(typeof(NonNullType<TenantType>))]
        // [UseProjection]
        [HotChocolate.Data.UseFiltering(typeof(TenantFilterInputType))]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Tenant> GetTenants([ScopedService] TenantFileContext tenantContext) => tenantContext.Tenants.AsQueryable();

        public async Task<Tenant> GetTenantAsync([ID(nameof(Tenant))] int id, TenantByIdDataLoader dataLoader, CancellationToken cancellationToken) => await dataLoader.LoadAsync(id, cancellationToken);
        public async Task<IEnumerable<Tenant>> GetTenantsAsync([ID(nameof(Tenant))] int[] ids, TenantByIdDataLoader dataLoader, CancellationToken cancellationToken) => await dataLoader.LoadAsync(ids, cancellationToken);
    }
}
