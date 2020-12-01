using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TenantFile.Api.Common;
using TenantFile.Api.Models;

namespace TenantFile.Api.DataLoader
{
    public interface IDataLoaderFactory<T> where T : class, IEntity
    {
        Func<IDbContextFactory<TenantFileContext>, DbSet<T>> dbSetFunc { get; init; }
        public DataLoaderById<T> CreateDataLoader(Func<TenantFileContext, DbSet<T>> func, IServiceProvider service);
        
    }

    public class DataLoaderFactory<T> : IDataLoaderFactory<T> where T : class, IEntity
    {
        public DataLoaderFactory(Func<IDbContextFactory<TenantFileContext>, DbSet<T>> func)
        {
            dbSetFunc = func;
        }

        public Func<IDbContextFactory<TenantFileContext>, DbSet<T>> dbSetFunc { get ; init; }

        public DataLoaderById<T> CreateDataLoader(IServiceProvider service)
        {
            var ctx = service.GetRequiredService<IDbContextFactory<TenantFileContext>>();
            return new DataLoaderById<T>(default!, ctx, func);
        }
    }
}
