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
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Phones
{
    [ExtendObjectType(Name = "Query")]
    public class PhoneQueries
    {

        [UseTenantFileContext]
        [UsePaging]
        //[UseSelection]
        [UseFiltering(FilterType = typeof(PhoneFilterInputType))]
        [UseSorting]
        public IQueryable<Phone> GetPhones([ScopedService] TenantFileContext tenantContext) => tenantContext.Phones.AsQueryable();

        [UseTenantFileContext]
        public async Task<List<Phone>> GetTenantlessPhonesAsync(
            [ScopedService] TenantFileContext tenantContext) =>
                    await tenantContext.Phones
                         .AsQueryable()
                         .Where(p => p.Tenants.Count == 0)//default for int is 0 so if Tenants==null, it should still return 0. Evaluating null Tenant list throws implementation exception
                         .ToListAsync();
                


    }
}
