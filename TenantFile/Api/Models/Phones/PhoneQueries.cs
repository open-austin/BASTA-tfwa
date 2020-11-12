using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;

namespace TenantFile.Api.Models.Phones
{
    [ExtendObjectType(Name = "Query")]
    public class PhoneQueries
    {

        [UseTenantFileContext]
        [UsePaging]
        //[UseSelection]
        //[UseFiltering]
        //[UseSorting]
        public IQueryable<Phone> GetPhones([ScopedService] TenantFileContext tenantContext) => tenantContext.Phones.AsQueryable();

        [UseTenantFileContext]
        public async Task<IEnumerable<Phone>> GetTenantlessPhonesAsync(
            [ScopedService] TenantFileContext tenantContext) =>
                    await tenantContext.Phones
                         .AsQueryable()
                         .Where(p => p.Tenants == null || p.Tenants.Count == 0)
                         .ToListAsync()
                         .ConfigureAwait(false);
                


    }
}
