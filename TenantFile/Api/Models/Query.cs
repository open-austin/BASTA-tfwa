using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace TenantFile.Api.Models
{
    public class Query
    {
        [UsePaging]
        [UseSelection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Tenant> GetTenants([Service] TenantContext tenantContext) => tenantContext.Tenants;

        [UsePaging]
        [UseSelection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Property> GetProperties([Service] TenantContext tenantContext) => tenantContext.Properties;
    }
}
