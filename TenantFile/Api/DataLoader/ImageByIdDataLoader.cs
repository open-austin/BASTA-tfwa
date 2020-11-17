using GreenDonut;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TenantFile.Api.Models;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.DataLoader
{
    public class ImageByIdDataLoader : BatchDataLoader<int, Image>
    {
        private readonly IDbContextFactory<TenantFileContext> dbContextFactory;

        public ImageByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<TenantFileContext> dbContextFactory)
            : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(this.dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Image>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using TenantFileContext dbContext =
                dbContextFactory.CreateDbContext();

            return await dbContext.Images.AsAsyncEnumerable()//or as qureyable?
               .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}