using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Data;
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
using HotChocolate.Types.Relay;

namespace TenantFile.Api.Models.Phones
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class PhoneQueries
    {

        [UseTenantFileContext]
        [UsePaging(typeof(NonNullType<PhoneType>))]
        // [UseProjection]
        [HotChocolate.Data.UseFiltering(typeof(PhoneFilterInputType))]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Phone> GetPhones([ScopedService] TenantFileContext tenantContext) => tenantContext.Phones.AsQueryable().Include(p => p.Tenants);

        public async Task<Phone> GetPhoneAsync([ID(nameof(Phone))] int id,
                                         PhoneByIdDataLoader dataLoader,
                                         CancellationToken cancellationToken) => await dataLoader.LoadAsync(id, cancellationToken);
        public async Task<IEnumerable<Phone>> GetPhonesAsync([ID(nameof(Phone))] int[] ids,
                                         PhoneByIdDataLoader dataLoader,
                                         CancellationToken cancellationToken) => await dataLoader.LoadAsync(ids, cancellationToken);

        [UseTenantFileContext]
        public async Task<List<Phone>> GetTenantlessPhonesAsync(
            [ScopedService] TenantFileContext tenantContext) =>
                    await tenantContext.Phones
                         .AsQueryable()
                         .Where(p => p.Tenants.Count == 0)//default for int is 0 so if Tenants==null, it should still return 0. Evaluating null Tenant list throws implementation exception
                         .ToListAsync();



    }
}
