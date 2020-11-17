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
    public class ResidenceByPropertyDataLoader : BatchDataLoader<int, Residence>
    {
        private readonly IDbContextFactory<TenantFileContext> dbContextFactory;

        public ResidenceByPropertyDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<TenantFileContext> dbContextFactory)
            : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(this.dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Residence>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using TenantFileContext dbContext =
                dbContextFactory.CreateDbContext();
            return await dbContext.Residences.AsAsyncEnumerable()//or as qureyable?
               .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}
