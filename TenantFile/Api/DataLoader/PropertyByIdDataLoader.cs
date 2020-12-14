using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using TenantFile.Api.Models;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.DataLoader
{
    public class PropertyByIdDataLoader : BatchDataLoader<int, Property>
    {
        private readonly IDbContextFactory<TenantFileContext> dbContextFactory;

        public PropertyByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<TenantFileContext> dbContextFactory)
            : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(this.dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Property>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using TenantFileContext dbContext =
                dbContextFactory.CreateDbContext();

            return await dbContext.Properties.AsQueryable()
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}