using System.Linq;
using HotChocolate;
using HotChocolate.Types;

namespace TenantFile.Api.Models
{
    public class Query
    {
        [UseSelection]
        public IQueryable<Tenant> GetTenants([Service] TenantContext tenantContext) => tenantContext.Tenants;

        [UseSelection]
        public IQueryable<Property> GetProperties([Service] TenantContext tenantContext) => tenantContext.Properties;
    }
}
