using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GreenDonut;
using HotChocolate.DataLoader;
using TenantFile.Api.Models;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.DataLoader
{
    public class TenantByIdDataLoader : BatchDataLoader<int, Tenant>
    {
        private readonly IDbContextFactory<TenantFileContext> dbContextFactory;

        public TenantByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<TenantFileContext> dbContextFactory)
            : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(this.dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Tenant>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using TenantFileContext dbContext =
                dbContextFactory.CreateDbContext();

            return await dbContext.Tenants.AsAsyncEnumerable()//or as qureyable?
               .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}