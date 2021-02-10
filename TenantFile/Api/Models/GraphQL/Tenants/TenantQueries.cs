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

namespace TenantFile.Api.Tenants
{
    [ExtendObjectType(Name = "Query")]
    public class TenantQueries
    {
        [UseTenantFileContext]
        [UsePaging]
        [UseProjection]
        [HotChocolate.Data.UseFiltering(typeof(TenantFilterInputType))]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Tenant> GetTenants([ScopedService] TenantFileContext tenantContext) => tenantContext.Tenants.AsQueryable();

        public Task<Tenant> GetTenantAsync([ID(nameof(Tenant))] int id, TenantByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
    }
}
