using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;
using HotChocolate.Types.Relay;

namespace TenantFile.Api.Models.Addresses
{
    [ExtendObjectType(Name = "Query")]
    public class AddressQueries
    {
        [UseTenantFileContext]
        [UsePaging]
        [HotChocolate.Data.UseFiltering(typeof(AddressFilterInputType))]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Address> GetAddresses([ScopedService] TenantFileContext tenantContext) => tenantContext.Addresses.AsQueryable().OrderBy(a => a.PostalCode);

        public Task<Address> GetAddressAsync([ID(nameof(Address))] int id, AddressByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);

    }
}
