
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;

namespace TenantFile.Api.Models
{
    //public class Query
    //{
    //    [UseTenantFileContext]
    //    [UsePaging]
    //    [UseSelection]
    //    [UseFiltering]
    //    [UseSorting]
    //    public IQueryable<Tenant> GetTenants([ScopedService] TenantFileContext tenantContext) => tenantContext.Tenants.AsQueryable<Tenant>().OrderBy(t => t.Name);

    //    [UseTenantFileContext]
    //    [UsePaging]
    //    [UseSelection]
    //    [UseFiltering]
    //    [UseSorting]
    //    public IQueryable<Property> GetProperties([ScopedService] TenantFileContext tenantContext) => tenantContext.Properties.AsQueryable<Property>().OrderBy(p => p.Name);

    //    [UseTenantFileContext]
    //    [UsePaging]
    //    [UseSelection]
    //    [UseFiltering]  
    //    [UseSorting]
    //    public IQueryable<Organizer> GetOrganizers([ScopedService] TenantFileContext tenantContext) => tenantContext.Organizers.AsQueryable<Organizer>().OrderBy(o => o.Name);

    //    //Should this be GetTenants or will the dataLoader batch the calls to the db when there are multiples?
    //    public Task<Tenant> GetTenantAsync(int id, TenantByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
    //}
}
