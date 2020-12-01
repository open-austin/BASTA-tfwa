using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using TenantFile.Api.Common;
using TenantFile.Api.Models;

namespace TenantFile.Api.DataLoader
{
    public class DataLoaderById<T> : BatchDataLoader<int, T> where T : class, IEntity
        {
            private readonly IDbContextFactory<TenantFileContext> dbContextFactory;
            private readonly Func<TenantFileContext, DbSet<T>> dbsetCreator;
            public DataLoaderById(
                    IBatchScheduler batchScheduler,
                    IDbContextFactory<TenantFileContext> dbContextFactory,
                    Func<TenantFileContext, DbSet<T>> dbset)
                    : base(batchScheduler)
            {
                dbsetCreator = dbset;
            this.dbContextFactory = dbContextFactory ??
                      throw new ArgumentNullException(nameof(this.dbContextFactory));
        }
        protected override async Task<IReadOnlyDictionary<int, T>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            //await using TenantFileContext dbContext =
            //    dbContextFactory.CreateDbContext();
            //var dbSetInfo = dbContext.GetType().GetProperties()
            //    .Where(c => c.PropertyType == typeof(DbSet<T>)).FirstOrDefault()!;
            //return await ((DbSet<T>)dbContext.GetType().InvokeMember(
            //    dbSetInfo.Name,
            //    System.Reflection.BindingFlags.GetProperty,
            //    binder: default,
            //    target: dbContext,
            //    null)!).AsQueryable()
            //                .Where(s => keys.Contains(s.Id))
            //                .ToDictionaryAsync(t => t.Id, cancellationToken);
            await using var ctx = dbContextFactory.CreateDbContext();
            return await dbsetCreator(ctx).AsAsyncEnumerable()
                   .Where(s => keys.Contains(s.Id))
                    .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
        }
    }